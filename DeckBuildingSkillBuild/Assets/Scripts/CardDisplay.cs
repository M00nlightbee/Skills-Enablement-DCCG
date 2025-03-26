using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBuildGame;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
	public Card cardData;
	public Image cardImage;
	public TMP_Text nameText;
	public TMP_Text descriptionText;
	public TMP_Text manaText;
	public TMP_Text effectText;
	public Image[] rarityImages;
	public Image[] typeImages;

	private Color[] rarityColors = { 
		Color.white, // common
		Color.green, // uncommon
		Color.blue, // rare
		new Color(0.5f, 0f, 0.5f), // epic
		Color.yellow // legendary
	};
	private Color[] typeColors = { 
		Color.red, // attack
		Color.green, // heal
		Color.blue // question
	};

	void Start()
	{
		UpdateCardDisplay();
	}

	public void UpdateCardDisplay()
	{
		// Update type image based on the card type
		for (int i = 0; i < typeImages.Length; i++)
		{
			if (i < cardData.cardType.Count)
			{
				typeImages[i].gameObject.SetActive(true);
				typeImages[i].color = typeColors[(int)cardData.cardType[i]];
			}
			else
			{
				typeImages[i].gameObject.SetActive(false);
			}
		}
		// Update rarity images based on the card rarity
		for (int i = 0; i < rarityImages.Length; i++)
		{
			if (i < cardData.cardRarity.Count)
			{
				rarityImages[i].gameObject.SetActive(true);
				rarityImages[i].color = rarityColors[(int)cardData.cardRarity[i]];
			}
			else
			{
				rarityImages[i].gameObject.SetActive(false);
			}
		}
		nameText.text = cardData.cardName;
		descriptionText.text = cardData.cardDescription;
		manaText.text = cardData.manaCost.ToString();
		effectText.text = cardData.effect.ToString();
		cardImage.sprite = cardData.cardImage;
	}

}
