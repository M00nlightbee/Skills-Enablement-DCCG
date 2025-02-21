using UnityEngine;
using UnityEngine.UI;

public class Enemy_Mana_Bar : MonoBehaviour
{
    public Image Mana_Bar_Image;
    public Enemy_Stats enemy_stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Mana_Bar_Image.fillAmount = (float)enemy_stats.mana/enemy_stats.max_mana;

        
    }
}
