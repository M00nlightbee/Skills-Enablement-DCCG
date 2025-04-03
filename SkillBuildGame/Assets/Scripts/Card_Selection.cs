using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Selection : MonoBehaviour , IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
	private Canvas canvas;
	private RectTransform canvasRectTranform;
	private Vector3 originalScale;
	private int currentState = 0;
	private Quaternion originalRotation;
	private Vector3 originalPosition;

	[SerializeField] private float selectScale = 1.1f;
	[SerializeField] private Vector2 cardPlay;
	[SerializeField] private Vector3 playPosition;
	public GameObject glowEffect;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();

		if (canvas != null)
		{
			canvasRectTranform = canvas.GetComponent<RectTransform>();
		}

		originalScale = rectTransform.localScale;
		originalPosition = rectTransform.localPosition;
		originalRotation = rectTransform.localRotation;

	}

	void Update()
	{
		
		switch (currentState)
		{
			case 1:
				HandleHoverState();
				break;
			case 2:
				if (!Input.GetMouseButton(0))
				{
					TransitionToState0();
				}
				break;
			case 3:
				HandlePlayState();
				if (!Input.GetMouseButton(0))
				{
					TransitionToState0();
				}
				break;
		}
	}

	public void TransitionToState0()
	{
		currentState = 0;
		rectTransform.localScale = originalScale; 
		rectTransform.localRotation = originalRotation; 
		rectTransform.localPosition = originalPosition; 
		glowEffect.SetActive(false); 
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (currentState == 0)
		{
			originalPosition = rectTransform.localPosition;
			originalRotation = rectTransform.localRotation;
			originalScale = rectTransform.localScale;

			currentState = 1;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (currentState == 1)
		{
			TransitionToState0();
		}
       
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (currentState == 1)
		{
			currentState = 2;
		}
	}

	public void HandleHoverState()
	{
		glowEffect.SetActive(true);
		rectTransform.localScale = originalScale * selectScale;
	}

	private void HandlePlayState()
	{
		rectTransform.localPosition = playPosition;
		rectTransform.localRotation = Quaternion.identity;

		if (Input.mousePosition.y < cardPlay.y)
		{
			currentState = 2;
		}
	}

}
