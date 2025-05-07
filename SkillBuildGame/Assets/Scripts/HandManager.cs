using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBuildGame;
using System;

public class HandManager : MonoBehaviour
{
	public GameObject cardPrefab;
	public Transform handTransform; //Root of the hand position
	public float fanSpread = 7.5f;
	public float cardSpacing = 100f;
	public float verticalSpacing = 100f;
	public int maxHandSize = 6;
	public List<GameObject> cardsInHand = new List<GameObject>();
	private DeckManager deckManager;

	void Start()
	{
		deckManager = FindAnyObjectByType<DeckManager>();
	}

	public void AddCardToHand(Card cardData)
	{
		if (cardsInHand.Count >= maxHandSize)
		{
			FindAnyObjectByType<MessageDisplay>().ShowMessage($"Max number of cards in hand {maxHandSize}, End turn", 4f);
			return;
		}

		// Instantiate the card
		GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
		cardsInHand.Add(newCard);

		// Ensure CanvasGroup component is added
		if (newCard.GetComponent<CanvasGroup>() == null)
		{
			newCard.AddComponent<CanvasGroup>();
		}

		// Set the CardData of the instantiated card
		newCard.GetComponent<CardDisplay>().cardData = cardData;

		UpdateHandVisuals();
	}

	public void RemoveCardFromHand(GameObject cardObject)
	{
		if (cardsInHand.Contains(cardObject))
		{
			cardsInHand.Remove(cardObject);
			Destroy(cardObject);
			UpdateHandVisuals();
		}
	}

	public void DrawCardOnClick()
	{
		deckManager.DrawCard(this);
	}

	void Update()
	{
		//UpdateHandVisuals();
	}

	private void UpdateHandVisuals()
	{
		int cardCount = cardsInHand.Count;

		if (cardCount == 1)
		{
			cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
			return;
		}

		for (int i = 0; i < cardCount; i++)
		{
			float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
			cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

			float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

			float normalizedPosition = (2f * i / (cardCount - 1) - 1f); //Normalize card position between -1, 1
			float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

			//Set card position
			cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
		}
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
