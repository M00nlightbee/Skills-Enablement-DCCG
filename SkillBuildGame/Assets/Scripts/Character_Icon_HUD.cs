using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class Character_Icon_HUD : MonoBehaviour
{

    public RawImage Character_Icon;
    public Texture[] Character_Array = new Texture[20];
    private int position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Read_CSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Read_CSV()
	{
		using (StreamReader reader = new StreamReader("Assets/Character_Choice.csv"))
		{
            position = Convert.ToInt32(reader.ReadLine());
            //Debug.Log(position);
			Character_Icon.texture = Character_Array[position];
		}
	}
}
