using SkillBuildGame;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHandManager : MonoBehaviour
{
	public GameObject cardPrefab; // Assign card prefab in inspector
	public List<GameObject> cardsInHand = new List<GameObject>();
	public int maxHandSize = 6;
	private DeckManager deckManager;
	public Transform opponentHandPanel; // Reference to the opponent's hand panel

	private void Start()
	{
		deckManager = FindAnyObjectByType<DeckManager>();
	}

	public void AddCardToHand(Card cardData)
	{
		if (cardsInHand.Count >= maxHandSize)
			return;

		// Instantiate the card as a child of the opponent's hand panel
		GameObject newCard = Instantiate(cardPrefab, opponentHandPanel);
		cardsInHand.Add(newCard);

		// Ensure CanvasGroup component is added
		CanvasGroup canvasGroup = newCard.GetComponent<CanvasGroup>();
		if (canvasGroup == null)
		{
			canvasGroup = newCard.AddComponent<CanvasGroup>();
		}

		// Disable interaction for the opponent's cards
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;

		// Set the CardData of the instantiated card
		newCard.GetComponent<CardDisplay>().cardData = cardData;
	}

	public void RemoveCardFromHand(GameObject cardObject)
	{
		if (cardsInHand.Contains(cardObject))
		{
			cardsInHand.Remove(cardObject);
			Destroy(cardObject);

			// Draw a new card after the opponent plays a card
			deckManager.DrawCardForOpponent(this);
		}
	}

	void Update()
	{
		// UpdateHandVisuals();
	}

	public void SetHandInteractable(bool interactable)
	{
		foreach (var card in cardsInHand)
		{
			CanvasGroup canvasGroup = card.GetComponent<CanvasGroup>();
			if (canvasGroup != null)
			{
				canvasGroup.interactable = interactable;
			}
			else
			{
				Debug.LogWarning("CanvasGroup component not found on card object.");
			}
		}
	}

}
