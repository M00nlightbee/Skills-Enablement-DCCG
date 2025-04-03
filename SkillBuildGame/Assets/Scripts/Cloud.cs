using UnityEngine;
using UnityEngine.SceneManagement;

public class Cloud : MonoBehaviour
{
    public void StartCloud()
    {
        SceneManager.LoadSceneAsync("ComingSoon");
    }   
}
