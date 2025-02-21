using System;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
	public int currentTurn = 1; // Tracks the current turn number
	public int currentPlayer = 0; // 0 for Player 1, 1 for Player 2
	public int multiplierCardCount = 0; // Tracks how many multiplier cards have been played
	public int turnsPerRound = 2; // Adjust this based on the number of players
	public Button endTurnButton; // Reference to the End Turn button
	public float turnTime = 120f; // 2 minutes in seconds
	private float timer;

	public event Action<int> OnTurnChanged; // Event to notify turn changes
	public event Action OnQuestionTriggered; // Event triggered when the question popup appears

	void Start()
	{
		if (endTurnButton != null)
		{
			endTurnButton.onClick.AddListener(End_Turn);
		}
		StartTurn();
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			End_Turn();
		}
	}

	public void StartTurn()
	{
		timer = turnTime;
		Debug.Log("Turn " + currentTurn + " - Player " + (currentPlayer + 1));
		OnTurnChanged?.Invoke(currentTurn);
	}

	public void End_Turn()
	{
		if (multiplierCardCount > 0)
		{
			TriggerQuestion();
			multiplierCardCount = 0; // Reset multiplier count after triggering a question
		}

		currentPlayer = (currentPlayer + 1) % turnsPerRound;
		if (currentPlayer == 0)
		{
			currentTurn++; // Increment turn after all players have played
		}

		StartTurn();
	}

	public void PlayMultiplierCard()
	{
		multiplierCardCount++;
	}

	private void TriggerQuestion()
	{
		Debug.Log("Question triggered! Answer to earn a card.");
		OnQuestionTriggered?.Invoke();
	}
}
