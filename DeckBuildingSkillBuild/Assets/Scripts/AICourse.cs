using UnityEngine;
using UnityEngine.SceneManagement;

public class AICourse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartAI()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

   
}
