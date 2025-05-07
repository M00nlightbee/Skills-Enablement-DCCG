using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ViewLeaderboard : MonoBehaviour
{
	public Button viewLeaderBoard;

	// Already setup in editor, only uncomment if you don't want to set it up in the editor
	//void Start()
	//{
	//	viewLeaderBoard.onClick.AddListener(OnViewLeaderboard);
	//}
	public void OnViewLeaderboard()
	{
		Debug.Log("View Leaderboard button clicked");
		SceneManager.LoadScene("LeaderBoard");
	}
}
