using UnityEngine;
using UnityEngine.UI;

public class Shield_Bar : MonoBehaviour
{
    public Image Shield_Bar_Image;
    public Image Health_Bar_Image;
    public Player_Stats player_Stats;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shield_Bar_Image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        Shield_Bar_Image.fillAmount = (float)player_Stats.shield/player_Stats.max_shield;

         
        
        Shield_Bar_Image.rectTransform.localPosition = new Vector3((int)(Health_Bar_Image.fillAmount * 150),0,0);
        
        
    }
}
