using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Return_Main_Menu : MonoBehaviour
{
    public GameObject End_Panel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {  
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
            Debug.Log("return");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
