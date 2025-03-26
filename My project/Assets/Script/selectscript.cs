using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecttScript : MonoBehaviour
{
    public void Select()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
