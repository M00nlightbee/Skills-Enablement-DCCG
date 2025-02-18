using UnityEngine;
using UnityEngine.UI;

public class Enemy_Change_Icon : MonoBehaviour
{
    public RawImage Character_Icon;
    public Texture[] Character_Array = new Texture[20];
    private int position;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            if (position >=0)
            {
            position --;
            Character_Icon.texture = Character_Array[position];
            }
        } 
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            if (position <20)
            {
            position ++;
            Character_Icon.texture = Character_Array[position];
            }
        } 
    }
}
