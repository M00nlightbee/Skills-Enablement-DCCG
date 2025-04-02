using UnityEngine;
using UnityEngine.EventSystems;
using SkillBuildGame;

public class DragUIObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
	private RectTransform rectTransform;
	private Canvas canvas;
	private Vector2 originalLocalPointerPosition;
	private Vector3 originalPanelLocalPosition;
	public float movementSensitivity = 1.0f; 
	private CardDisplay cardDisplay;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>(); 
		cardDisplay = GetComponent<CardDisplay>(); 
	}

	public void OnPointerDown(PointerEventData eventData) //This is inherited from the IPointerDownHandler class referenced above
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition); //Using the event system to detect what is clicked on
		originalPanelLocalPosition = rectTransform.localPosition;
	}

	public void OnDrag(PointerEventData eventData) //This is inherited from the IDragHandler class referenced above
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition))
		{
			localPointerPosition /= canvas.scaleFactor;

			// Adjusting the movement based on sensitivity
			Vector3 offsetToOriginal = (localPointerPosition - originalLocalPointerPosition) * movementSensitivity;
			rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// holder for future code
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		DropArea dropArea = other.GetComponent<DropArea>();
		if (dropArea != null)
		{
			if (cardDisplay != null && cardDisplay.cardData != null)
			{
				Card card = cardDisplay.cardData;
				if (card.cardType.Contains(Card.CardType.question))
				{
					GameManager.Instance.HandleQuestionCardDrop(card, gameObject);
				}
				else if (card.cardType.Contains(Card.CardType.attack))
				{
					GameManager.Instance.DealDamageToOpponent(card, gameObject);
				}
				else if (card.cardType.Contains(Card.CardType.heal))
				{
					GameManager.Instance.HealPlayer(card, gameObject);
				}
			}
		}
	}
}
