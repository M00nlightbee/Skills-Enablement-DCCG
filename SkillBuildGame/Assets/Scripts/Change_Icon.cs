using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Change_Icon : MonoBehaviour
{

    public RawImage Character_Icon;
    public Texture[] Character_Array = new Texture[20];
    private int position;
    public Button Next_button;
    public Button Back_button;
    public Button Select_button;
    void Start()
    {
        Back_button.onClick.AddListener(BackOption);
        Next_button.onClick.AddListener(NextOption);
        Select_button.onClick.AddListener(Write_To_File);
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

    void Write_To_File()
    {

        using (StreamWriter sw = new StreamWriter("Assets/Character_Choice.csv", false))
            {
                sw.Write(position);
                sw.Close();
            }

    }


}
