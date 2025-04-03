using UnityEngine;
using TMPro;


public class MessageDisplay : MonoBehaviour
{
	public TextMeshProUGUI messageText;

	void Start()
	{
		messageText.text = ""; 
	}

	public void ShowMessage(string message, float duration)
	{
		messageText.text = message;
		Invoke("ClearMessage", duration);
	}

	void ClearMessage()
	{
		messageText.text = "";
	}
}
