using UnityEngine;
using UnityEngine.UI;

public class Mana_Bar : MonoBehaviour
{

    public Image Mana_Bar_Image;
    public Player_Stats player_Stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Mana_Bar_Image.fillAmount = (float)player_Stats.mana/player_Stats.max_mana;

         
    }
}
