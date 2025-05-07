using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
	// Links for further reading to use supabase:
	//https://supabase.com/docs/guides/getting-started/ai-prompts/database-rls-policies
	//https://medium.com/@lemapp09/supabase-with-unity-c-291c71116806
	//https://supabase.com/docs
	[SerializeField] private string leaderboardUrl = "copy url from supabase followed by/rest/v1/scores";
	[SerializeField] private string apiKey = "copy api key from supabase ";


	void Start()
	{
		Debug.Log("Initial Leaderboard URL: " + leaderboardUrl);
	}

	public void UploadTime(string player_name, float time)
	{
		StartCoroutine(PostTime(player_name, time));
	}

	private IEnumerator PostTime(string player_name, float time)
	{
		LeaderboardData data = new LeaderboardData(player_name, time);
		string json = JsonUtility.ToJson(data);
		UnityWebRequest request = new UnityWebRequest(leaderboardUrl, "POST");
		byte[] jsonSent = System.Text.Encoding.UTF8.GetBytes(json);
		request.uploadHandler = new UploadHandlerRaw(jsonSent);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		request.SetRequestHeader("Authorization", "Bearer " + apiKey);
		request.SetRequestHeader("apiKey", apiKey);

		Debug.Log("Uploading JSON: " + json);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			Debug.Log("Time uploaded successfully");
		}
		else
		{
			Debug.LogError("Error uploading time: " + request.error);
			Debug.LogError("Server Response: " + request.downloadHandler.text);
		}
	}

	public void FetchLeaderboard(System.Action<List<LeaderboardData>> onDataLoad)
	{
		StartCoroutine(GetTopTime(onDataLoad));
	}

	private IEnumerator GetTopTime(System.Action<List<LeaderboardData>> callback)
	{
		string url = leaderboardUrl + "?apikey=" + apiKey + "&select=player_name,time&order=time.asc&limit=10";

		Debug.Log("Leaderboard URL: " + leaderboardUrl);
		Debug.Log("Full URL: " + url);

		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SetRequestHeader("apikey", apiKey);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			string json = request.downloadHandler.text;
			Debug.Log("Response: " + json);

			LeaderboardData[] dataArray = JsonHelper.FromJson<LeaderboardData>(json);
			callback?.Invoke(new List<LeaderboardData>(dataArray));
		}
		else
		{
			Debug.LogError("Error fetching leaderboard: " + request.error);
			Debug.LogError("Response: " + request.downloadHandler.text);
		}
	}

	// https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
	public static class JsonHelper
	{
		public static T[] FromJson<T>(string json)
		{
			string newJson = "{\"Items\":" + json + "}";
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
			return wrapper.Items;
		}

		[System.Serializable]
		private class Wrapper<T>
		{
			public T[] Items;
		}
	}

}
