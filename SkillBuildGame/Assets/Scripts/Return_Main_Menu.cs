using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Return_Main_Menu : MonoBehaviour
{
    public GameObject End_Panel;
    
    void Update()
    {  
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
            Debug.Log("return");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
