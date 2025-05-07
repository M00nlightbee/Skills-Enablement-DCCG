using UnityEngine;

[System.Serializable]
public class LeaderboardData
{
	public string player_name;
	public float time;
	public LeaderboardData(string player_name, float time)
	{
		this.player_name = player_name;
		this.time = time;
	}
}
