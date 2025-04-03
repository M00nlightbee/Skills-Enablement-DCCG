using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SkillBuildGame;


public class Save_Card_Selection : MonoBehaviour
{
	public GameObject shown_deck;
	public Button Custom_Button;
	public TextMeshProUGUI button_text;
	public bool[] Card_Selected = new bool[18];
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Custom_Button.onClick.AddListener(Custom_Deck_Save);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Custom_Deck_Save()
	{

		Deck_Data_Collector deck_Data = shown_deck.GetComponent<Deck_Data_Collector>();

		deck_Data.collection_text.text = "Custom Collection";

		for (int i = 0; i < deck_Data.allCards.Count; i++)
		{
			deck_Data.Card_Selected[i] = Card_Selected[i];
		}



	}


}