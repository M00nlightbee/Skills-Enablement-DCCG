using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
	private LeaderboardManager manager;
	public GameObject leaderboardEntryPrefab;
	public Transform leaderboardContainer;

	public void Start()
	{
		// Find the LeaderboardManager in the scene
		manager = FindAnyObjectByType<LeaderboardManager>();

		if (manager == null)
		{
			Debug.LogError("LeaderboardManager not found in the scene. Please ensure it is added.");
			return;
		}

		RefreshLeaderBoard();
	}

	public void RefreshLeaderBoard()
	{
		foreach (Transform child in leaderboardContainer)
		{
			if (child.gameObject != leaderboardEntryPrefab)
				Destroy(child.gameObject);
		}
		manager.FetchLeaderboard((onDataLoad) =>
		{
			int playerRank = 1;
			foreach (var data in onDataLoad)
			{
				GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
				TextMeshProUGUI text = entry.GetComponentInChildren<TextMeshProUGUI>();
				text.text = $"{playerRank}. {data.player_name} - {data.time:F2} seconds";
				playerRank++;
			}
		});
	}
}
