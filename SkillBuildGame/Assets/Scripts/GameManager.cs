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
	private OpponentHandManager opponentHandManager;
	private bool isPlayerTurn = true;
	//public GameObject bulletPrefab;
	//public Transform bulletSpawn;

	// Mana System
	private int playerMana = 3;
	private int opponentMana = 3;
	private const int maxMana = 10;
	private const int manaRegen = 2;
	private ManaUI playerManaUI;
	private ManaUI opponentManaUI;

	public Button endTurnButton;
	public Button drawCardButton;
	private DeckManager deckManager;

	private static System.Random random = new System.Random();

	// Reference to the Shoot component
	private Shoot shootComponent;

	// Reference to the AudioSource component
	private AudioSource audioSource;

	// Sound effect for dealing damage
	public AudioClip damageSoundEffect;

	// Reference to the opponent's ship image
	private Image opponentShip;

	public Text turnIndicatorText;

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
			opponentHandManager = FindAnyObjectByType<OpponentHandManager>();
			deckManager = FindAnyObjectByType<DeckManager>();

			// Set initial health values
			playerHealthUI.UpdateHealthUI(playerHealth);
			opponentHealthUI.UpdateHealthUI(opponentHealth);

			// Set initial mana values
			playerManaUI.UpdateManaUI(playerMana);
			opponentManaUI.UpdateManaUI(opponentMana);

			// Assign the button click event
			endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
			if (endTurnButton == null)
			{
				Debug.LogError("EndTurnButton is not assigned in the Inspector.");
				return;
			}
			endTurnButton.onClick.AddListener(OnEndTurnButtonClick);

			// Find the Shoot component
			shootComponent = FindAnyObjectByType<Shoot>();
			if (shootComponent == null)
			{
				Debug.LogError("Shoot component not found in the scene.");
				return;
			}

			// Find the AudioSource component
			audioSource = GetComponent<AudioSource>();
			if (audioSource == null)
			{
				Debug.LogError("AudioSource component not found on GameManager.");
				return;
			}

			// Find the opponent's ship image
			opponentShip = GameObject.Find("OpponentShip").GetComponent<Image>();
			if (opponentShip == null)
			{
				Debug.LogError("OpponentShip image not found in the scene.");
				return;
			}

			turnIndicatorText = GameObject.Find("TurnIndicatorText").GetComponent<Text>();
			UpdateTurnIndicator();

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
		GameObject cardObject = Instantiate(opponentHandManager.cardPrefab);

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

		// Set the card's position and parent
		cardObject.transform.SetParent(opponentHandManager.opponentHandPanel, false);
		cardObject.transform.localPosition = Vector3.zero;

		// Call EndTurn with the drawn card and cardObject
		EndTurn(drawnCard, cardObject);
	}

	private IEnumerator OpponentPlayRandomCards()
	{
		// Disable player's hand UI
		handManager.SetHandInteractable(false);

		// Disable the draw card button
		drawCardButton.interactable = false;

		// Disable end turn button
		endTurnButton.interactable = false;

		// Get all attack and heal cards from the opponent's hand
		List<Card> attackAndHealCards = opponentHandManager.cardsInHand
			.Select(cardObject => cardObject.GetComponent<CardDisplay>().cardData)
			.Where(card => card.cardType.Contains(Card.CardType.attack) || card.cardType.Contains(Card.CardType.heal))
			.ToList();

		Debug.Log("Opponent has " + attackAndHealCards.Count + " attack and heal cards.");

		bool cardPlayed = false;

		// Shuffle the list and take a random card
		List<Card> randomCards = attackAndHealCards.OrderBy(x => random.Next()).Take(1).ToList();

		foreach (Card card in randomCards)
		{
			GameObject cardObject = opponentHandManager.cardsInHand
				.FirstOrDefault(obj => obj.GetComponent<CardDisplay>().cardData == card);

			// Ensure CanvasGroup component is added
			if (cardObject.GetComponent<CanvasGroup>() == null)
			{
				cardObject.AddComponent<CanvasGroup>();
			}

			// Check if opponent has enough mana to play the card
			if (opponentMana >= card.manaCost)
			{
				// Play the card
				if (card.cardType.Contains(Card.CardType.attack))
				{
					// Opponent plays attack card in player's drop area
					SpawnCardInDropArea(card, cardObject, "PlayerDropArea");

					// Delete cardObject placed on PlayerDropArea after 1 second
					yield return new WaitForSeconds(1);
					Destroy(GameObject.Find("PlayerDropArea").transform.GetChild(1).gameObject);

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

					yield return new WaitForSeconds(1);
					Destroy(GameObject.Find("OpponentDropArea").transform.GetChild(1).gameObject);
				}

				cardPlayed = true;
			}
		}

		if (!cardPlayed)
		{
			Debug.Log("Opponent could not play any card. Switching turn back to player.");
		}

		UpdatePlayerMana();

		// Re-enable player's hand UI
		handManager.SetHandInteractable(true);

		// Re-enable the draw card button
		drawCardButton.interactable = true;

		// Enable end turn button
		endTurnButton.interactable = true;

		// Switch turn back to player
		isPlayerTurn = true;

		yield break;
	}

	public void EndTurn(Card card, GameObject cardObject)
	{
		isPlayerTurn = !isPlayerTurn; // Switch turn
		UpdateTurnIndicator(); // Show turn indicator for 2 secs

		if (isPlayerTurn)
		{
			opponentHandManager.SetHandInteractable(false);
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
			UpdateOpponentMana();
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
				//// Ensure bulletSpawn is assigned
				//if (bulletSpawn == null)
				//{
				//    Debug.LogError("bulletSpawn is not assigned.");
				//    return;
				//}

				// Call the Shoot method to spawn a bullet
				shootComponent.ShootBullet();

				// Play the damage sound effect
				if (damageSoundEffect != null)
				{
					audioSource.PlayOneShot(damageSoundEffect);
				}

				// Make the opponent's ship image glow red
				StartCoroutine(GlowOpponentShip(Color.red, 0.5f));

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

	private IEnumerator GlowOpponentShip(Color color, float duration)
	{
		Color originalColor = opponentShip.color;
		opponentShip.color = color;
		yield return new WaitForSeconds(duration);
		opponentShip.color = originalColor;
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
		}
		else
		{
			Debug.Log("Card is not of type heal or player's health is not less than maxHealth. No healing applied.");
		}
	}

	public void HandleQuestionCardDrop(Card card, GameObject cardObject)
	{
		if (card.cardType.Contains(Card.CardType.question))
		{
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

			// Set the number of questions to ask based on the card's effect value
			Questions.totalQuestionsToAsk = card.effect;

			Debug.Log($"Total Questions to Ask set to: {Questions.totalQuestionsToAsk}");

			//// Ensure the cardDisplay is set correctly
			//Questions questionsComponent = FindAnyObjectByType<Questions>();
			//if (questionsComponent != null)
			//{
			//    questionsComponent.cardDisplay = cardObject.GetComponent<CardDisplay>();
			//    Debug.Log($"CardDisplay set with effect: {questionsComponent.cardDisplay.cardData.effect}");
			//}

			handManager.RemoveCardFromHand(cardObject);
			Destroy(cardObject);
		}
		else
		{
			Debug.Log("Unable to load QuestionScene");
		}

		// Pause the game
		Time.timeScale = 0;

		// Load the question scene additively
		SceneManager.LoadSceneAsync("Question_Display", LoadSceneMode.Additive);
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

	private void UpdateTurnIndicator()
	{
		if (turnIndicatorText != null)
		{
			turnIndicatorText.gameObject.SetActive(true); // Show the indicator
			turnIndicatorText.text = isPlayerTurn ? "Your Turn!" : "Opponent's Turn!";
			turnIndicatorText.color = isPlayerTurn ? Color.green : Color.red;

			// Start coroutine to hide after 2 seconds
			StartCoroutine(HideTurnIndicatorAfterDelay());
		}
	}

	private IEnumerator HideTurnIndicatorAfterDelay()
	{
		yield return new WaitForSeconds(1f);
		turnIndicatorText.gameObject.SetActive(false);
	}

	// update player's mana 
	public void UpdatePlayerMana()
	{
		// add to player's mana with number of correct answers
		playerMana += manaRegen;
		playerMana = Mathf.Clamp(playerMana, 0, maxMana);
		playerManaUI.UpdateManaUI(playerMana);
	}

	// update player's mana with number of correct answers
	public void UpdatePlayerManaWithCorrectAnswers()
	{
		// Add to player's mana with the number of correct answers
		playerMana += Questions.correctAnswers;
		playerMana = Mathf.Clamp(playerMana, 0, maxMana);
		playerManaUI.UpdateManaUI(playerMana);

		// Reset correctAnswers after updating mana
		Questions.correctAnswers = 0;
	}

	// updating opponent's mana
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


