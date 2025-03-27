using System.Collections;
using System.Collections.Generic;
using SkillBuildGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	private int playerHealth = 20;
	private int opponentHealth = 30;
	private const int minHealth = 0;
	private const int maxHealth = 30;
	private const string defeatSceneName = "DefeatScene";
	private const string victorySceneName = "VictoryScene";
	private HealthUI playerHealthUI;
	private HealthUI opponentHealthUI;
	private HandManager handManager;
	private bool isPlayerTurn = true;

	// Mana System
	private int playerMana = 3;
	private int opponentMana = 3;
	private const int maxMana = 10;
	private const int manaRegen = 2;
	private ManaUI playerManaUI;
	private ManaUI opponentManaUI;

	public Button endTurnButton;
	private DeckManager deckManager;

	private static System.Random random = new System.Random();

	//reference number of correct answers by player from Questions.cs
	private int correctAnswers = Questions.correctAnswers;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			playerHealthUI = GameObject.Find("PlayerHealthText").GetComponent<HealthUI>();
			opponentHealthUI = GameObject.Find("OpponentHealthText").GetComponent<HealthUI>();
			playerManaUI = GameObject.Find("PlayerManaText").GetComponent<ManaUI>();
			opponentManaUI = GameObject.Find("OpponentManaText").GetComponent<ManaUI>();
			handManager = FindAnyObjectByType<HandManager>();
			deckManager = FindAnyObjectByType<DeckManager>();

			// Set initial health values
			playerHealthUI.UpdateHealthUI(playerHealth);
			opponentHealthUI.UpdateHealthUI(opponentHealth);

			// Set initial mana values
			playerManaUI.UpdateManaUI(playerMana);
			opponentManaUI.UpdateManaUI(opponentMana);

			// Assign the button click event
			endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
			endTurnButton.onClick.AddListener(OnEndTurnButtonClick);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnEndTurnButtonClick()
	{
		// Draw a card from the deck
		Card drawnCard = deckManager.allCards[random.Next(0, deckManager.allCards.Count)];
		GameObject cardObject = Instantiate(handManager.cardPrefab);

		// Ensure CanvasGroup component is added
		if (cardObject.GetComponent<CanvasGroup>() == null)
		{
			cardObject.AddComponent<CanvasGroup>();
		}

		// Set the card data on the CardDisplay component
		CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
		if (cardDisplay != null)
		{
			cardDisplay.SetCard(drawnCard);
		}
		else
		{
			Debug.LogError("CardDisplay component not found on card object.");
		}

		// Call EndTurn with the drawn card and cardObject
		EndTurn(drawnCard, cardObject);
	}

	private IEnumerator OpponentPlayRandomCards()
	{
		// Disable player's hand UI
		handManager.SetHandInteractable(false);

		// Get all attack and heal cards from the deck
		List<Card> attackAndHealCards = deckManager.allCards
			.Where(card => card.cardType.Contains(Card.CardType.attack) || card.cardType.Contains(Card.CardType.heal))
			.ToList();

		// Shuffle the list and take 3 random cards
		List<Card> randomCards = attackAndHealCards.OrderBy(x => random.Next()).Take(3).ToList();

		foreach (Card card in randomCards)
		{
			GameObject cardObject = Instantiate(handManager.cardPrefab);

			// Ensure CanvasGroup component is added
			if (cardObject.GetComponent<CanvasGroup>() == null)
			{
				cardObject.AddComponent<CanvasGroup>();
			}

			CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
			if (cardDisplay != null)
			{
				cardDisplay.SetCard(card);
			}
			else
			{
				Debug.LogError("CardDisplay component not found on card object.");
			}

			//check if opponent has enough mana to play the card
			if (opponentMana >= card.manaCost)
			{
				// Play the card
				if (card.cardType.Contains(Card.CardType.attack))
				{
					// Opponent plays attack card in player's drop area
					SpawnCardInDropArea(card, cardObject, "PlayerDropArea");
					// Update player's health
					playerHealth -= card.effect;
					playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
					playerHealthUI.UpdateHealthUI(playerHealth);
					// Update opponent's mana
					opponentMana -= card.manaCost;
					opponentMana = Mathf.Clamp(opponentMana, 0, maxMana);
					opponentManaUI.UpdateManaUI(opponentMana);
					// Check health status
					CheckHealthStatus();
				}
				else if (card.cardType.Contains(Card.CardType.heal))
				{
					// Opponent plays heal card in opponent's drop area
					SpawnCardInDropArea(card, cardObject, "OpponentDropArea");
					// Update opponent's health
					opponentHealth += card.effect;
					opponentHealth = Mathf.Clamp(opponentHealth, 0, maxHealth);
					opponentHealthUI.UpdateHealthUI(opponentHealth);
					// Update opponent's mana
					opponentMana -= card.manaCost;
					opponentMana = Mathf.Clamp(opponentMana, 0, maxMana);
					opponentManaUI.UpdateManaUI(opponentMana);
					// Check health status
					CheckHealthStatus();
				}
				// Remove the card from hand and destroy it after a delay
				yield return StartCoroutine(RemoveAndDestroyCardAfterDelay(cardObject, 1.0f));
				// Wait for a short delay between each card play
				yield return new WaitForSeconds(1.0f);
			}
		}

		// Re-enable player's hand UI
		handManager.SetHandInteractable(true);
	}

	public void EndTurn(Card card, GameObject cardObject)
	{
		// Switch turn
		isPlayerTurn = !isPlayerTurn;

		// Check card type and spawn in the appropriate area
		if (isPlayerTurn)
		{
			if (card.cardType.Contains(Card.CardType.attack))
			{
				DealDamageToOpponent(card, cardObject);
			}
			else if (card.cardType.Contains(Card.CardType.heal))
			{
				HealPlayer(card, cardObject);
			}
			else if (card.cardType.Contains(Card.CardType.question))
			{
				HandleQuestionCardDrop(card, cardObject);
			}

			UpdateOpponentMana();
		}
		else
		{
			StartCoroutine(OpponentPlayRandomCards());
		}
	}

	private void SpawnCardInDropArea(Card card, GameObject cardObject, string areaName)
	{
		// Find the drop area by name
		Transform dropArea = GameObject.Find(areaName).transform;

		// Instantiate the card in the drop area
		GameObject newCard = Instantiate(cardObject, dropArea.position, Quaternion.identity, dropArea);
		newCard.GetComponent<CardDisplay>().SetCard(card);
	}

	private IEnumerator RemoveAndDestroyCardAfterDelay(GameObject cardObject, float delay)
	{
		yield return new WaitForSeconds(delay);

		// Remove the card from hand and destroy it
		handManager.RemoveCardFromHand(cardObject);
		Destroy(cardObject);
	}

	public void DealDamageToOpponent(Card card, GameObject cardObject)
	{
		// Check if the card type before dealing damage
		if (card.cardType.Contains(Card.CardType.attack))
		{
			// check if player has enough mana before dealing damage
			if (!CheckMana(card))
			{
				Debug.Log("Not enough mana to play the card.");
				return;
			}
			else
			{
				opponentHealth -= card.effect;
				opponentHealth = Mathf.Clamp(opponentHealth, 0, maxHealth);
				Debug.Log($"Dealing {card.effect} damage to the opponent's ship. New health: {opponentHealth}");
				opponentHealthUI.UpdateHealthUI(opponentHealth);

				// Update player's mana
				playerMana -= card.manaCost;
				playerMana = Mathf.Clamp(playerMana, 0, maxMana);
				playerManaUI.UpdateManaUI(playerMana);

				// Check health status
				CheckHealthStatus();

				// Remove the card from hand and destroy it
				handManager.RemoveCardFromHand(cardObject);
				Destroy(cardObject);
			}

		}
		else
		{
			Debug.Log("Card is not of type attack. No action taken.");
		}
	}

	public void HealPlayer(Card card, GameObject cardObject)
	{
		// Check if the card type is heal and player's health is less than maxHealth
		if (card.cardType.Contains(Card.CardType.heal) && playerHealth < maxHealth)
		{
			// check if player has enough mana before healing
			if (!CheckMana(card))
			{
				Debug.Log("Not enough mana to play the card.");
				return;
			}
			else
			{
				playerHealth += card.effect;
				playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
				Debug.Log($"Healing player by {card.effect}. New health: {playerHealth}");
				playerHealthUI.UpdateHealthUI(playerHealth);

				// Update player's mana after healing
				playerMana -= card.manaCost;
				playerMana = Mathf.Clamp(playerMana, 0, maxMana);
				playerManaUI.UpdateManaUI(playerMana);

				// Check health status
				CheckHealthStatus();

				// Remove the card from hand and destroy it
				handManager.RemoveCardFromHand(cardObject);
				Destroy(cardObject);
			}
	;
		}
		else
		{
			Debug.Log("Card is not of type heal or player's health is not less than maxHealth. No healing applied.");
		}
	}

	public void HandleQuestionCardDrop(Card card, GameObject cardObject)
	{
		// check if card dropped is of type question
		if (card.cardType.Contains(Card.CardType.question))
		{
			// check if player has enough mana before loading the question scene
			if (!CheckMana(card))
			{
				Debug.Log("Not enough mana to play the card.");
				return;
			}

			Debug.Log("Loading QuestionScene...");

			// Update player's mana
			playerMana -= card.manaCost;
			playerMana = Mathf.Clamp(playerMana, 0, maxMana);
			playerManaUI.UpdateManaUI(playerMana);

			GetPlayerMana();

			// Set the number of questions to ask based on the card's mana cost value
			Questions.totalQuestionsToAsk = card.manaCost == 0 ? 2 : card.manaCost;

			StartCoroutine(LoadQuestionScene());

			handManager.RemoveCardFromHand(cardObject);
			Destroy(cardObject);
		}
		else
		{
			Debug.Log("Unable to load QuestionScene");
		}
	}

	private IEnumerator LoadQuestionScene()
	{
		// Pause the game
		Time.timeScale = 0;

		// Load the question scene additively
		yield return SceneManager.LoadSceneAsync("Question_Display", LoadSceneMode.Additive);
	}

	private void CheckHealthStatus()
	{
		if (playerHealth <= minHealth)
		{
			SceneManager.LoadSceneAsync(defeatSceneName);
		}
		else if (opponentHealth <= minHealth)
		{
			SceneManager.LoadSceneAsync(victorySceneName);
		}
	}

	//// update player's mana after answering questions
	//public void UpdatePlayerMana()
	//{
	//	// add to player's mana with number of correct answers
	//	playerMana += correctAnswers;
	//	playerMana = Mathf.Clamp(playerMana, 0, maxMana);
	//	playerManaUI.UpdateManaUI(playerMana);
	//}

	// Handle updating opponent's mana function
	public void UpdateOpponentMana()
	{
		opponentMana += manaRegen;
		opponentMana = Mathf.Clamp(opponentMana, 0, maxMana);
		opponentManaUI.UpdateManaUI(opponentMana);
	}

	// Chcek player has enough mana to play the card
	public bool CheckMana(Card card)
	{
		if (playerMana >= card.manaCost)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int GetPlayerHealth()
	{
		return playerHealth;
	}
	public int GetOpponentHealth()
	{
		return opponentHealth;
	}
	public int GetPlayerMana()
	{
		return playerMana;
	}
	public int GetOpponentMana()
	{
		return opponentMana;
	}
}


