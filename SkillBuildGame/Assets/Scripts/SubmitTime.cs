using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SubmitTime : MonoBehaviour
{
	public TMP_InputField inputField;
	public Button submitButton;
	public Button viewLeaderBoard;
	public TMP_Text confirmationText;
	LeaderboardManager manager;
	public TMP_Text victoryTimeText;
	public static float PlayerTime { get; private set; }
	void Start()
	{
		// Already setup in editor, only uncomment if you don't want to set it up in the editor
		//submitButton.onClick.AddListener(OnSubmit);
		//viewLeaderBoard.onClick.AddListener(OnViewLeaderboard);
		manager = FindAnyObjectByType<LeaderboardManager>();

		if (manager == null)
		{
			Debug.LogWarning("LeaderboardManager not found in scene.");
		}
	}
	public void OnSubmit()
	{
		string playerName = inputField.text;

		if (string.IsNullOrEmpty(playerName))
		{
			confirmationText.text = "Please enter your name !";
			confirmationText.color = Color.red;
			return;
		}

		// Parse the time string from victoryTimeText to store as float in database
		if (victoryTimeText != null)
		{
			string timeText = victoryTimeText.text.Replace(" seconds", "");
			if (float.TryParse(timeText, out float parsedTime))
			{
				PlayerTime = parsedTime;
			}
			else
			{
				Debug.LogError("Failed to parse time from victoryTimeText.");
				confirmationText.text = "Error: Invalid time!";
				confirmationText.color = Color.red;
				return;
			}
		}
		else
		{
			Debug.LogError("victoryTimeText is not assigned.");
			confirmationText.text = "Error: Time not found!";
			confirmationText.color = Color.red;
			return;
		}

		confirmationText.text = "Submitting...";
		confirmationText.color = Color.grey;

		manager.UploadTime(playerName, PlayerTime);
		confirmationText.text = "Submitted successfully!";
		confirmationText.color = Color.green;
	}

	public void OnViewLeaderboard()
	{
		Debug.Log("View Leaderboard button clicked");
		SceneManager.LoadScene("LeaderBoard");
	}
}
