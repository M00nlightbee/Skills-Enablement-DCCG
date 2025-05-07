using UnityEngine;
using TMPro;

public class VictorySceneManager : MonoBehaviour
{
	public TMP_Text victoryTimeText;
	void Start()
	{
		// Retrieve the elapsed time from GameManager and display it
		float elapsedTime = GameManager.ElapsedTimeToVictory;
		victoryTimeText.text = $" {elapsedTime:F2} seconds";
	}
}
