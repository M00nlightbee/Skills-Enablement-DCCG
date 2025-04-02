using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBuildGame;


public class DeckManager : MonoBehaviour
{
	public List<Card> allCards = new List<Card>(); // Full deck
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

		// Draw starting hand for player
		for (int i = 0; i < startingHandSize; i++)
		{
			DrawCard(handManager);
			DrawCardForOpponent(opponentHandManager);
		}

		// Draw starting hand for opponent
		//for (int i = 0; i < startingHandSize; i++)
		//{
		//	DrawCardForOpponent(opponentHandManager);
		//}

		// Ensure there's at least one Question card in hand
		// EnsureQuestionCardInHand();
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

		//EnsureQuestionCardInHand();
	}

	// draw card for opponent hand
	public void DrawCardForOpponent(OpponentHandManager handManager)
	{
		//if (allCards.Count == 0 || handManager.cardsInHand.Count >= handManager.maxHandSize)
		//	return;

		//Card nextCard = allCards[currentIndex];
		//handManager.AddCardToHand(nextCard);
		//currentIndex = (currentIndex + 1) % allCards.Count;

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

	//private void EnsureQuestionCardInHand()
	//{
	//	bool hasQuestionCard = handManager.cardsInHand.Exists(card =>
	//		card.GetComponent<CardDisplay>().cardData.cardType.Contains(Card.CardType.question));

	//	if (!hasQuestionCard)
	//	{
	//		Card questionCard = GetQuestionCard();
	//		if (questionCard != null)
	//		{
	//			handManager.AddCardToHand(questionCard);
	//		}
	//	}
	//}
}

