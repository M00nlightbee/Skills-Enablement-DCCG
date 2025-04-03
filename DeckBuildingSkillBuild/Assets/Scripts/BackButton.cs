using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoBack()
    {
        SceneManager.LoadSceneAsync("ChooseCourse");
    }

   
}
