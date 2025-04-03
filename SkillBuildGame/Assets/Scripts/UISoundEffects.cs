using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public AudioClip hoverSound;
	public AudioClip clickSound;
	private AudioManager audioManager;
	private Image buttonImage;
	private Color originalColor;
	public Color hoverColor = Color.gray;

	private void Start()
	{
		audioManager = FindAnyObjectByType<AudioManager>();
		if (audioManager == null)
		{
			Debug.LogError("AudioManager not found in the scene!");
		}

		buttonImage = GetComponent<Image>();
		if (buttonImage != null)
		{
			originalColor = buttonImage.color;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (hoverSound != null && audioManager != null)
		{
			audioManager.PlayClip(hoverSound);
			ChangeColor(hoverColor);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (clickSound != null && audioManager != null)
		{
			audioManager.PlayClip(clickSound);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ResetColor();
	}

	private void ChangeColor(Color newColor)
	{
		if (buttonImage != null)
		{
			buttonImage.color = newColor;
		}
	}

	private void ResetColor()
	{
		if (buttonImage != null)
		{
			buttonImage.color = originalColor;
		}
	}
}
