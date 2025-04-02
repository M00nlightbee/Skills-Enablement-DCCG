using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collection_Loader : MonoBehaviour
{

    public Button Collection_Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collection_Button.onClick.AddListener(Collection_Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Collection_Click()
    {

        SceneManager.LoadScene("DeckBuilder");

    }
}
