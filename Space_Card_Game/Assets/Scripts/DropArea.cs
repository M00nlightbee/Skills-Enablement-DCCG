using UnityEngine;
using UnityEngine.EventSystems;
using SkillBuildGame;

public class DropArea : MonoBehaviour, IDropHandler
{
	public bool isPlayerDropArea; // tick in input if this is the player drop area

	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppedObject = eventData.pointerDrag;
		if (droppedObject != null)
		{
			CardDisplay cardDisplay = droppedObject.GetComponent<CardDisplay>();
			if (cardDisplay != null)
			{
				if (isPlayerDropArea)
				{
					// Check if the card type is heal and apply healing to the player
					if (cardDisplay.cardData.cardType.Contains(Card.CardType.heal))
					{
						GameManager.Instance.HealPlayer(cardDisplay.cardData);
					}
					else
					{
						Debug.Log("Card is not of type heal. No healing applied.");
					}
				}
				else
				{
					// Deal damage to the opponent's ship
					if (cardDisplay.cardData.cardType.Contains(Card.CardType.attack))
					{
						GameManager.Instance.DealDamageToOpponent(cardDisplay.cardData, droppedObject);
					}
					else
					{
						Debug.Log("Card is not of type attack. No damage dealt.");
					}
				}

				Debug.Log($"Card dropped - Type: {cardDisplay.cardData.cardType}, Name: {cardDisplay.cardData.cardName}, Effect: {cardDisplay.cardData.effect}");
			}
		}
	}
}
