using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBuildGame;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;


public class Deck_Data_Collector : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public GameObject[] Card_Slots = new GameObject[8];

    public Button Left_Button;
    public Button Right_Button;

    public Button New_Deck_Button;
    public Button Default_Deck_Button;

    public Button Custom_Deck_Button;

    public Button Back_Button;
    public int page_number = 0;
    public TextMeshProUGUI page_text;

    public TextMeshProUGUI collection_text;

    public bool[] Card_Selected = new bool[18];

    public Card_Selection[] card_Selection = new Card_Selection[8];



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load all card assets from the Resources folder
		Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
		allCards.AddRange(cards);

        // Set the card data on the CardDisplay component
        for(int i = 0; i <8; i++)
        {
		CardDisplay cardDisplay = Card_Slots[i].GetComponent<CardDisplay>();
        card_Selection[i] = Card_Slots[i].GetComponent<Card_Selection>();
		if (cardDisplay != null)
		{
			cardDisplay.SetCard(allCards[i]);
		}
		else
		{
			Debug.LogError("CardDisplay component not found on card object.");
		}


        }

        //Sets click events for direction button
        Left_Button.onClick.AddListener(Left_Button_Click);
        Right_Button.onClick.AddListener(Right_Button_Click);
        New_Deck_Button.onClick.AddListener(New_Deck_Click);
        Default_Deck_Button.onClick.AddListener(Default_Deck_Click);
        Back_Button.onClick.AddListener(Back_Click);
        
        Card_Slots[0].GetComponent<Button>().onClick.AddListener(() => Active_Selection(0));
        Card_Slots[1].GetComponent<Button>().onClick.AddListener(() => Active_Selection(1));
        Card_Slots[2].GetComponent<Button>().onClick.AddListener(() => Active_Selection(2));
        Card_Slots[3].GetComponent<Button>().onClick.AddListener(() => Active_Selection(3));
        Card_Slots[4].GetComponent<Button>().onClick.AddListener(() => Active_Selection(4));
        Card_Slots[5].GetComponent<Button>().onClick.AddListener(() => Active_Selection(5));
        Card_Slots[6].GetComponent<Button>().onClick.AddListener(() => Active_Selection(6));
        Card_Slots[7].GetComponent<Button>().onClick.AddListener(() => Active_Selection(7));

        Custom_Deck_Button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //will show if selected or not
        
        for(int i = 0; i < 8; i++)
        {
             if((i+(8*page_number)) < allCards.Count)
            {
            if(Card_Selected[i+(8*page_number)] == true)
            {
                card_Selection[i].GetComponent<Card_Selection>().glowEffect.SetActive(true); 
            }
            else
            {
                card_Selection[i].GetComponent<Card_Selection>().glowEffect.SetActive(false);
            }

            }
           
        }

        page_text.text = "Page " + (page_number+1);
        
    }

    void Left_Button_Click()
    {
        if(page_number > 0)
        {
            page_number--;
        }

        // Set the card data on the CardDisplay component
        for(int i = 0; i <8; i++)
        {

            if(Card_Slots[i].activeSelf == false)
        {
            Card_Slots[i].SetActive(true);
        }
		CardDisplay cardDisplay = Card_Slots[i].GetComponent<CardDisplay>();
		if (cardDisplay != null)
		{
			cardDisplay.SetCard(allCards[i+(8*page_number)]);
		}
		else
		{
			Debug.LogError("CardDisplay component not found on card object.");
		}


        }   

    }

    void Right_Button_Click()
    {
        if(page_number < 2)
        {
            page_number++;
        }



        // Set the card data on the CardDisplay component
        for(int i = 0; i <8; i++)
        {

            if((i+(8*page_number)) >= allCards.Count)
        {
            Card_Slots[i].SetActive(false);
        }
        else
        {
            CardDisplay cardDisplay = Card_Slots[i].GetComponent<CardDisplay>();
		if (cardDisplay != null)
		{
			cardDisplay.SetCard(allCards[i+(8*page_number)]);
		}
		else
		{
			Debug.LogError("CardDisplay component not found on card object.");
		}
        }

        } 
        
    }

    void Active_Selection(int i)
    {
        if(Card_Selected[i+(8*page_number)] == true)
        {
            Card_Selected[i+(8*page_number)] = false;
        }
        else
        {
            Card_Selected[i+(8*page_number)] = true;
        }
       
    }

    void New_Deck_Click()
    {
        Custom_Deck_Button.gameObject.SetActive(true);
        Save_Card_Selection save_Card = Custom_Deck_Button.GetComponent<Save_Card_Selection>();

         for(int i = 0; i <allCards.Count; i++)
        {
            save_Card.Card_Selected[i] = Card_Selected[i];
            Card_Selected[i] = false;
        }

    }

    void Default_Deck_Click()
    {

        collection_text.text = "Default Collection";
        for(int i = 0; i <allCards.Count; i++)
        {
            Card_Selected[i] = true;
        }
    }

    void Back_Click()
    {

        SceneManager.LoadScene("ChooseCourse");

    }

}
