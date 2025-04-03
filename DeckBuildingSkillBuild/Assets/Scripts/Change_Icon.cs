using UnityEngine;
using UnityEngine.UI;

public class Change_Icon : MonoBehaviour
{

    public RawImage Character_Icon;
    public Texture[] Character_Array = new Texture[20];
    private int position;
     public Button Next_button;
    public Button Back_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Back_button.onClick.AddListener(BackOption);
        Next_button.onClick.AddListener(NextOption);
    }

    // Update is called once per frame
    void Update()
    {

    }
     public void NextOption()
    {
       if (position <20)
            {
            position ++;
            Character_Icon.texture = Character_Array[position];
            }
    }

    public void BackOption()
    {
       if (position >=0)
            {
            position --;
            Character_Icon.texture = Character_Array[position];
            }
    }
}
