using UnityEngine;

public class CountDown : MonoBehaviour
{
	public TextMesh timeTextMesh;

	private float totalTime;

	private float timeRemaining;

	void Start()
	{
		if (timeTextMesh != null)
		{
			string textVal = timeTextMesh.text;

			if (textVal.Contains(":"))
			{
				string[] parts = textVal.Split(':');
				if (parts.Length == 2 && int.TryParse(parts[0], out int minutes) && int.TryParse(parts[1], out int seconds))
				{
					totalTime = minutes * 60 + seconds;
				}
				else
				{
					Debug.LogError("Invalid time format in TextMesh. Please use MM:SS format (e.g., 2:00). Defaulting to 2 minutes.");
					totalTime = 120f;
				}
			}
			else
			{
				if (!float.TryParse(textVal, out totalTime))
				{
					Debug.LogError("Invalid numeric time value in TextMesh. Defaulting to 2 minutes (120 seconds).");
					totalTime = 120f;
				}
			}
		}
		else
		{
			Debug.LogError("No TextMesh assigned for the timer. Defaulting totalTime to 2 minutes (120 seconds).");
			totalTime = 120f;
		}

		// Initialize the countdown.
		timeRemaining = totalTime;
		UpdateTimerDisplay();
	}

	void Update()
	{
		// Count down only if there is time remaining.
		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
			if (timeRemaining < 0)
				timeRemaining = 0;

			UpdateTimerDisplay();
		}
	}

	// Update the TextMesh with the remaining time in MM:SS format.
	void UpdateTimerDisplay()
	{
		if (timeTextMesh != null)
		{
			int minutes = Mathf.FloorToInt(timeRemaining / 60);
			int seconds = Mathf.FloorToInt(timeRemaining % 60);
			timeTextMesh.text = string.Format("{0:0}:{1:00}", minutes, seconds);
		}
	}
}
