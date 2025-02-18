using UnityEngine;
using UnityEngine.UI;


public class Health_Bar : MonoBehaviour
{

    public Image Health_Bar_Image;

    public Player_Stats player_Stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health_Bar_Image.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

        Health_Bar_Image.fillAmount = (float)player_Stats.health/player_Stats.max_health;    

    }
}
