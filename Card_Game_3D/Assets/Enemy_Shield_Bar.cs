using UnityEngine;
using UnityEngine.UI;

public class Enemy_Shield_Bar : MonoBehaviour
{
    public Image Shield_Bar_Image;
    public Image Health_Bar_Image;
     public Enemy_Stats enemy_stats;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shield_Bar_Image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        Shield_Bar_Image.fillAmount = (float)enemy_stats.shield/enemy_stats.max_shield;
 
        
        Shield_Bar_Image.rectTransform.localPosition = new Vector3((int)(-125 +((1-Health_Bar_Image.fillAmount) * 150)),0,0);
        
        
    }
}
