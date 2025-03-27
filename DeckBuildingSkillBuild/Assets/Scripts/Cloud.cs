using UnityEngine;
using UnityEngine.SceneManagement;

public class Cloud : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartCloud()
    {
        SceneManager.LoadSceneAsync("ComingSoon");
    }

    
}
