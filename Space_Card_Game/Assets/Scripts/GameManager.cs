using System.Collections;
using System.Collections.Generic;
using SkillBuildGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//public static GameManager Instance { get; private set; }

	//private int playerHealth;
	//private int playerManaCount;
	//private int opponentHealth;
	//private int opponentManaCount;

	//private int playerXP;

	//public OptionsManager OptionsManager { get; private set; }
	//public AudioManager AudioManager { get; private set; }
	//public DeckManager DeckManager { get; private set; }

	//private void Awake()
	//{
	//	if (Instance == null)
	//	{
	//		Instance = this;
	//		DontDestroyOnLoad(gameObject);
	//		InitializeManagers();
	//	}
	//	else if(Instance != this)
	//	{
	//		Destroy(gameObject);
	//	}
	//}

	//private void InitializeManagers()
	//{
	//	OptionsManager = GetComponentInChildren<OptionsManager>();
	//	AudioManager = GetComponentInChildren<AudioManager>();
	//	DeckManager = GetComponentInChildren<DeckManager>();

	//	if (OptionsManager == null)
	//	{
	//		GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
	//		if (prefab == null)
	//		{
	//			Debug.Log($"OptionsManager prefab not found");
	//		}
	//		else
	//		{
	//			Instantiate(prefab, transform.position, Quaternion.identity, transform);
	//			OptionsManager = GetComponentInChildren<OptionsManager>();
	//		}
	//	}
	//	if (AudioManager == null)
	//	{
	//		GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
	//		if (prefab == null)
	//		{
	//			Debug.Log($"AudioManager prefab not found");
	//		}
	//		else
	//		{
	//			Instantiate(prefab, transform.position, Quaternion.identity, transform);
	//			AudioManager = GetComponentInChildren<AudioManager>();
	//		}
	//	}
	//	if (DeckManager == null)
	//	{
	//		GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
	//		if (prefab == null)
	//		{
	//			Debug.Log($"DeckManager prefab not found");
	//		}
	//		else
	//		{
	//			Instantiate(prefab, transform.position, Quaternion.identity, transform);
	//			DeckManager = GetComponentInChildren<DeckManager>();
	//		}
	//	}
	//}

	//public int PlayerHealth
	//{
	//	get { return playerHealth; }
	//	set { playerHealth = value; }
	//}

	//public int PlayerManaCount
	//{
	//	get { return playerManaCount; }
	//	set { playerManaCount = value; }
	//}

	//public int PlayerXP 
	//{
	//	get { return playerXP; }
	//	set { playerXP = value; }
	//}

	//public int OpponentHealth
	//{
	//	get { return opponentHealth; }
	//	set { opponentHealth = value; }
	//}

	//public int OpponentManaCount
	//{
	//	get { return opponentManaCount; }
	//	set { opponentManaCount = value; }
	//}

	public static GameManager Instance { get; private set; }

	private int playerHealth = 30;
	private int opponentHealth = 20;
	private const int minHealth = 0;
	private const int maxHealth = 30;
	private const string defeatSceneName = "DefeatScene";
	private const string victorySceneName = "VictoryScene";
	private HealthUI playerHealthUI;
	private HealthUI opponentHealthUI;
	private HandManager handManager;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			playerHealthUI = GameObject.Find("PlayerHealthText").GetComponent<HealthUI>();
			opponentHealthUI = GameObject.Find("OpponentHealthText").GetComponent<HealthUI>();
			handManager = FindAnyObjectByType<HandManager>();

			// Set initial health values
			playerHealthUI.UpdateHealthUI(playerHealth);
			opponentHealthUI.UpdateHealthUI(opponentHealth);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void DealDamageToOpponent(Card card, GameObject cardObject)
	{
		// Check if the card type is attack before dealing damage
		if (card.cardType.Contains(Card.CardType.attack))
		{
			
			opponentHealth -= card.effect;
			opponentHealth = Mathf.Clamp(opponentHealth, 0, maxHealth); // Ensure health does not exceed maxHealth
			Debug.Log($"Dealing {card.effect} damage to the opponent's ship. New health: {opponentHealth}");
			opponentHealthUI.UpdateHealthUI(opponentHealth);

			// Check health status
			CheckHealthStatus();

			// Remove the card from hand and destroy it
			handManager.RemoveCardFromHand(cardObject);
		}
		else if (card.cardType.Contains(Card.CardType.question))
		{
			// Change the scene if the card type is question
			SceneManager.LoadScene("Question_Display",LoadSceneMode.Additive); // Bug: not loading the scene
		}
		else
		{
			Debug.Log("Card is not of type attack or question. No action taken.");
		}
	}

	public void HealPlayer(Card card)
	{
		// Check if the card type is heal and player's health is less than maxHealth
		if (card.cardType.Contains(Card.CardType.heal) && playerHealth < maxHealth)
		{
			playerHealth += card.effect;
			playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth); // Ensure health does not exceed maxHealth
			Debug.Log($"Healing player by {card.effect}. New health: {playerHealth}");
			playerHealthUI.UpdateHealthUI(playerHealth);

			// Check health status
			CheckHealthStatus();
		}
		else
		{
			Debug.Log("Card is not of type heal or player's health is not less than maxHealth. No healing applied.");
		}
	}

	private void CheckHealthStatus()
	{
		if (playerHealth <= minHealth)
		{
			SceneManager.LoadScene(defeatSceneName,LoadSceneMode.Additive);
		}
		else if (opponentHealth <= minHealth)
		{
			SceneManager.LoadScene(victorySceneName,LoadSceneMode.Additive);
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
}
