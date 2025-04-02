using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBuildGame;


public class DeckManager : MonoBehaviour
{
	public List<Card> allCards = new List<Card>();
	public int startingHandSize = 4;
	private int currentIndex = 0;
	public int maxHandSize;
	public int currentHandSize;
	private HandManager handManager;
	private OpponentHandManager opponentHandManager;

	void Start()
	{
		// Load all card assets from the Resources folder
		Card[] cards = Resources.LoadAll<Card>("Cards");
		allCards.AddRange(cards);

		handManager = FindAnyObjectByType<HandManager>();
		opponentHandManager = FindAnyObjectByType<OpponentHandManager>();
		maxHandSize = handManager.maxHandSize;

		// Draw starting hand
		for (int i = 0; i < startingHandSize; i++)
		{
			DrawCard(handManager);
			DrawCardForOpponent(opponentHandManager);
		}
	}

	void Update()
	{
		if (handManager != null)
		{
			currentHandSize = handManager.cardsInHand.Count;
		}
		if (opponentHandManager != null)
		{
			currentHandSize = opponentHandManager.cardsInHand.Count;
		}
	}

	public void DrawCard(HandManager handManager)
	{
		if (allCards.Count == 0 || currentHandSize >= maxHandSize)
			return;

		Card nextCard = allCards[currentIndex];
		handManager.AddCardToHand(nextCard);
		currentIndex = (currentIndex + 1) % allCards.Count;
	}

	// draw card for opponent hand
	public void DrawCardForOpponent(OpponentHandManager handManager)
	{
		if (allCards.Count == 0 || currentHandSize >= maxHandSize)
			return;

		Card nextCard = allCards[currentIndex];
		handManager.AddCardToHand(nextCard);
		currentIndex = (currentIndex + 1) % allCards.Count;
	}

	public Card GetQuestionCard()
	{
		return allCards.Find(card => card.cardType.Contains(Card.CardType.question));
	}
}

