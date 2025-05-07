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

	private int playerHealth = 30;
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

	private Shoot shootComponent;

	private AudioManager audioManager;
	public AudioClip damageSoundEffect;
	public AudioClip healSoundEffect;


	private Image opponentShip;
	private Image playerShip;
	public Text turnIndicatorText;

	public static float ElapsedTimeToVictory { get; private set; }

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

			endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
			endTurnButton.onClick.AddListener(OnEndTurnButtonClick);

			shootComponent = FindAnyObjectByType<Shoot>();

			audioManager = FindAnyObjectByType<AudioManager>();

			opponentShip = GameObject.Find("OpponentShip").GetComponent<Image>();
			playerShip = GameObject.Find("PlayerShip").GetComponent<Image>();

			turnIndicatorText = GameObject.Find("TurnIndicatorText").GetComponent<Text>();
			UpdateTurnIndicator();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public GameObject targetGameObject;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (targetGameObject != null)
			{
				// Toggle the active state of the GameObject
				bool isActive = targetGameObject.activeSelf;
				targetGameObject.SetActive(!isActive);
				Debug.Log($"ESC key pressed. GameObject active state: {!isActive}");
			}
			else
			{
				Debug.LogWarning("Target GameObject is not assigned in the Inspector.");
			}
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

		cardObject.transform.SetParent(opponentHandManager.opponentHandPanel, false);
		cardObject.transform.localPosition = Vector3.zero;

		EndTurn(drawnCard, cardObject);
	}

	private IEnumerator OpponentPlayRandomCards()
	{
		// Disable player's UI
		handManager.SetHandInteractable(false);
		drawCardButton.interactable = false;
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
			FindAnyObjectByType<MessageDisplay>().ShowMessage("Opponent could not play any card. Switching turn back to player", 3f);
			
		}

		UpdatePlayerMana();

		// Re-enable player's UI
		handManager.SetHandInteractable(true);
		drawCardButton.interactable = true;
		endTurnButton.interactable = true;

		// Switch turn back to player
		isPlayerTurn = true;
		UpdateTurnIndicator();

		yield break;
	}

	public void EndTurn(Card card, GameObject cardObject)
	{
		isPlayerTurn = !isPlayerTurn; // Switch turn
		UpdateTurnIndicator();

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
		Transform dropArea = GameObject.Find(areaName).transform;

		GameObject newCard = Instantiate(cardObject, dropArea.position, Quaternion.identity, dropArea);
		newCard.GetComponent<CardDisplay>().SetCard(card);
	}

	public void DealDamageToOpponent(Card card, GameObject cardObject)
	{
		if (card.cardType.Contains(Card.CardType.attack))
		{
			if (!CheckMana(card))
			{
				Debug.Log("Not enough mana to play the card.");
				FindAnyObjectByType<MessageDisplay>().ShowMessage(" Not enough mana to play the card ", 3f);
				return;
			}
			else
			{
				shootComponent.ShootBullet();
				// Play the damage sound effect using AudioManager
				audioManager.PlayClip(damageSoundEffect);

				opponentHealth -= card.effect;
				opponentHealth = Mathf.Clamp(opponentHealth, 0, maxHealth);
				Debug.Log($"Dealing {card.effect} damage to the opponent's ship. New health: {opponentHealth}");
				opponentHealthUI.UpdateHealthUI(opponentHealth);

				// Make the opponent's ship image glow red
				StartCoroutine(GlowShip(opponentShip, Color.red, 0.5f));

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
			FindAnyObjectByType<MessageDisplay>().ShowMessage(" Card is not of type attack. No action taken ", 3f);
		}
	}

	private IEnumerator GlowShip(Image ship, Color color, float duration)
	{
		Color originalColor = ship.color;
		float halfDuration = duration / 2f;

		for (int i = 0; i < 2; i++)
		{
			// Fade in
			for (float t = 0; t < halfDuration; t += Time.deltaTime)
			{
				ship.color = Color.Lerp(originalColor, color, t / halfDuration);
				yield return null;
			}
			ship.color = color;

			// Fade out
			for (float t = 0; t < halfDuration; t += Time.deltaTime)
			{
				ship.color = Color.Lerp(color, originalColor, t / halfDuration);
				yield return null;
			}
			ship.color = originalColor;
		}
	}

	private void AddGlowEffect(Button button)
	{
		var outline = button.gameObject.GetComponent<Outline>();
		if (outline == null)
		{
			outline = button.gameObject.AddComponent<Outline>();
		}
		outline.effectColor = Color.green;
		outline.effectDistance = new Vector2(8, 8);
	}

	private void RemoveGlowEffect(Button button)
	{
		var outline = button.gameObject.GetComponent<Outline>();
		if (outline != null)
		{
			Destroy(outline);
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
				FindAnyObjectByType<MessageDisplay>().ShowMessage(" Not enough mana to play the card ", 3f);
				return;
			}
			else
			{
	
				playerHealth += card.effect;
				playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
				Debug.Log($"Healing player by {card.effect}. New health: {playerHealth}");
				playerHealthUI.UpdateHealthUI(playerHealth);

				audioManager.PlayClip(healSoundEffect);
				StartCoroutine(GlowShip(playerShip, Color.green, 0.5f));

				// Update player's mana after healing
				playerMana -= card.manaCost;
				playerMana = Mathf.Clamp(playerMana, 0, maxMana);
				playerManaUI.UpdateManaUI(playerMana);

				CheckHealthStatus();

				// Remove the card from hand and destroy it
				handManager.RemoveCardFromHand(cardObject);
				Destroy(cardObject);
			}
		}
		else
		{
			FindAnyObjectByType<MessageDisplay>().ShowMessage(" Max Health. No healing applied ", 3f);
		}
	}

	public void HandleQuestionCardDrop(Card card, GameObject cardObject)
	{
		if (card.cardType.Contains(Card.CardType.question))
		{
			if (!CheckMana(card))
			{
				FindAnyObjectByType<MessageDisplay>().ShowMessage(" Not enough mana to play the card ", 3f);
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

			handManager.RemoveCardFromHand(cardObject);
			Destroy(cardObject);
		}
		else
		{
			Debug.Log("Unable to load QuestionScene");
		}

		// Pause the game
		Time.timeScale = 0;
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
			ElapsedTimeToVictory = Time.timeSinceLevelLoad;
			SceneManager.LoadSceneAsync(victorySceneName);
		}
	}

	private void UpdateTurnIndicator()
	{
		if (turnIndicatorText != null)
		{
			turnIndicatorText.gameObject.SetActive(true); 
			turnIndicatorText.text = isPlayerTurn ? "Your Turn!" : "Opponent's Turn!";
			turnIndicatorText.color = isPlayerTurn ? Color.green : Color.red;

			// Add glow effect to the end turn button when it's the player's turn
			if (isPlayerTurn)
			{
				AddGlowEffect(endTurnButton);
				AddGlowEffect(drawCardButton);
			}
			else
			{
				RemoveGlowEffect(endTurnButton);
				RemoveGlowEffect(drawCardButton);
			}
			StopAllCoroutines();
			StartCoroutine(HideTurnIndicatorAfterDelay());
		}
	}


	private IEnumerator HideTurnIndicatorAfterDelay()
	{
		yield return new WaitForSeconds(2f);
		turnIndicatorText.gameObject.SetActive(false);
	}

	// update player's mana 
	public void UpdatePlayerMana()
	{
		playerMana += manaRegen;
		playerMana = Mathf.Clamp(playerMana, 0, maxMana);
		playerManaUI.UpdateManaUI(playerMana);
	}

	// update player's mana with number of correct answers
	public void UpdatePlayerManaWithCorrectAnswers()
	{
		playerMana += Questions.correctAnswers;
		playerMana = Mathf.Clamp(playerMana, 0, maxMana);
		playerManaUI.UpdateManaUI(playerMana);

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