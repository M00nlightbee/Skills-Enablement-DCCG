using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your scene name!
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked!");
        // You can later show a settings menu here
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Works in build, not editor
    }
}
