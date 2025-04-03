using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Screen : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("End_Screen_Scene",LoadSceneMode.Additive);
        }
    }
}
