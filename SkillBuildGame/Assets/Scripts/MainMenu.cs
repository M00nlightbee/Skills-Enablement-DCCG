using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
}
