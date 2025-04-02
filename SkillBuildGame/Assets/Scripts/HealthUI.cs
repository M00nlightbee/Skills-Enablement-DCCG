using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
	public TMP_Text healthText;

	public void UpdateHealthUI(int health)
	{
		healthText.text = health.ToString();
	}
}
