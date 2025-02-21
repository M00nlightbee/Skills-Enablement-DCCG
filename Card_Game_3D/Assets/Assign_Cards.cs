using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assign_Cards : MonoBehaviour
{

    public Button draw_button;
    public Sprite[] card_icons = new Sprite[52];
    public SpriteRenderer[] cards = new SpriteRenderer[5];

    private List<string> card_string_list = new List<string>();
    private List<string> player_hand = new List<string>();
    
    private int random_int;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 52; i++)
        {
            card_string_list.Add("Card "+ i);
        }
        draw_button.onClick.AddListener(Click_Trigger);
        
    }

    // Update is called once per frames
    void Update()
    {
        
    }

    void Click_Trigger()
    {
        Debug.Log("Press");

        for(int i = 0; i < 5; i++)
        {
            random_int = Random.Range(0,card_string_list.Count);
            player_hand.Add(card_string_list[random_int]);
            Debug.Log(player_hand[i]);
            cards[i].sprite = card_icons[random_int];
        }
    }
    
}
