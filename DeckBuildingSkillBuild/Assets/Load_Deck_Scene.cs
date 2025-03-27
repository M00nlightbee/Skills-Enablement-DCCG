using UnityEngine;
using UnityEngine.UI;

public class Load_Deck_Scene : MonoBehaviour
{


    public Button collection_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collection_button.onClick.AddListener(() =>
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("DeckBuilder");
		});
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
