using UnityEngine;
using UnityEngine.SceneManagement;

public class CyberSecurity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartCyber()
    {
       SceneManager.LoadSceneAsync("ComingSoon");
    }

    
}
