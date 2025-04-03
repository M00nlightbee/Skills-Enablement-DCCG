using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class Character_Icon_HUD : MonoBehaviour
{
	public RawImage Character_Icon;
	public Texture[] Character_Array = new Texture[20];
	private int position;

	void Start()
	{
		Read_Position();
	}

	void Read_Position()
	{
		if (PlayerPrefs.HasKey("CharacterPosition"))
		{
			position = PlayerPrefs.GetInt("CharacterPosition");

			if (position >= 0 && position < Character_Array.Length)
			{
				Character_Icon.texture = Character_Array[position];
			}
			else
			{
				Debug.LogError("Character position out of range: " + position);
			}
		}
		else
		{
			Debug.LogError("Character position not found in PlayerPrefs.");
		}
	}
}

