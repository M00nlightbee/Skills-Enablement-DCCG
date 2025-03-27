//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using SkillBuildGame;

//public class DeckManager : MonoBehaviour
//{
//	public List<Card> allCards = new List<Card>();

//	public int startingHandSize = 4;

//	private int currentIndex = 0;
//	public int maxHandSize;
//	public int currentHandSize;
//	private HandManager handManager;

//	void Start()
//	{
//		//Load all card assets from the Resources folder
//		Card[] cards = Resources.LoadAll<Card>("Cards");

//		//Add the loaded cards to the allCards list
//		allCards.AddRange(cards);

//		handManager = FindAnyObjectByType<HandManager>();
//		maxHandSize = handManager.maxHandSize;
//		for (int i = 0; i < startingHandSize; i++)
//		{
//			Debug.Log($"Drawing Card");
//			DrawCard(handManager);
//		}

//	}

//	void Update()
//	{
//		if (handManager != null)
//		{
//			currentHandSize = handManager.cardsInHand.Count;
//		}
//	}

//	public void DrawCard(HandManager handManager)
//	{
//		if (allCards.Count == 0)
//			return;

//		if (currentHandSize < maxHandSize)
//		{
//			Card nextCard = allCards[currentIndex];
//			handManager.AddCardToHand(nextCard);
//			currentIndex = (currentIndex + 1) % allCards.Count;
//		}
//	}
//}

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

	void Start()
	{
		// Load all card assets from the Resources folder
		Card[] cards = Resources.LoadAll<Card>("Cards");
		allCards.AddRange(cards);

		handManager = FindAnyObjectByType<HandManager>();
		maxHandSize = handManager.maxHandSize;

		// Draw starting hand
		for (int i = 0; i < startingHandSize; i++)
		{
			DrawCard(handManager);
		}

		// Ensure there's at least one Question card in hand
		EnsureQuestionCardInHand();
	}

	void Update()
	{
		if (handManager != null)
		{
			currentHandSize = handManager.cardsInHand.Count;
		}
	}

	public void DrawCard(HandManager handManager)
	{
		if (allCards.Count == 0 || currentHandSize >= maxHandSize)
			return;

		Card nextCard = allCards[currentIndex];
		handManager.AddCardToHand(nextCard);
		currentIndex = (currentIndex + 1) % allCards.Count;

		EnsureQuestionCardInHand();
	}

	public Card GetQuestionCard()
	{
		return allCards.Find(card => card.cardType.Contains(Card.CardType.question));
	}

	private void EnsureQuestionCardInHand()
	{
		bool hasQuestionCard = handManager.cardsInHand.Exists(card =>
			card.GetComponent<CardDisplay>().cardData.cardType.Contains(Card.CardType.question));

		if (!hasQuestionCard)
		{
			Card questionCard = GetQuestionCard();
			if (questionCard != null)
			{
				handManager.AddCardToHand(questionCard);
			}
		}
	}
}

