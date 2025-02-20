using UnityEngine;
using UnityEngine.UI;


public class Enemy_Health_Bar : MonoBehaviour
{

    public Image Health_Bar_Image;
   
    public Enemy_Stats enemy_stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        Health_Bar_Image.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        Health_Bar_Image.fillAmount = (float)enemy_stats.health/enemy_stats.max_health;
   

    }
}
