using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel; // ← This will connect to your UI panel in the Inspector

    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseCourse"); // Replace with your actual scene name
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked!");
        optionsPanel.SetActive(true); // 🔥 This shows the panel
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false); // 🔥 This hides the panel when "Back" is clicked
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
