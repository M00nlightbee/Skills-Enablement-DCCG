using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collection_Loader : MonoBehaviour
{

    public Button Collection_Button;
    void Start()
    {
        Collection_Button.onClick.AddListener(Collection_Click);
    }

    void Collection_Click()
    {
        SceneManager.LoadScene("DeckBuilder");
    }
}
