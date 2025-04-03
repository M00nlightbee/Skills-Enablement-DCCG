using SkillBuildGame;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHandManager : MonoBehaviour
{
	public GameObject cardPrefab;
	public List<GameObject> cardsInHand = new List<GameObject>();
	public int maxHandSize = 6;
	private DeckManager deckManager;
	public Transform opponentHandPanel; 

	private void Start()
	{
		deckManager = FindAnyObjectByType<DeckManager>();
	}

	public void AddCardToHand(Card cardData)
	{
		if (cardsInHand.Count >= maxHandSize)
			return;

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

		newCard.GetComponent<CardDisplay>().cardData = cardData;
	}

	public void RemoveCardFromHand(GameObject cardObject)
	{
		if (cardsInHand.Contains(cardObject))
		{
			cardsInHand.Remove(cardObject);
			Destroy(cardObject);
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
