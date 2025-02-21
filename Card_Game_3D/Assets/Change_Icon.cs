using UnityEngine;
using UnityEngine.UI;

public class Change_Icon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public RawImage Character_Icon;
    public Texture[] Character_Array = new Texture[20];
    private int position;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (position >=0)
            {
            position --;
            Character_Icon.texture = Character_Array[position];
            }
        } 
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (position <20)
            {
            position ++;
            Character_Icon.texture = Character_Array[position];
            }
        } 
    }
}
