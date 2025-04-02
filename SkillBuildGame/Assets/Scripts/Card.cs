using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillBuildGame
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class Card : ScriptableObject
	{		
		public string cardName;
		public List<CardRarity> cardRarity;
		public int manaCost;
		public string cardDescription;
		public Sprite cardImage;
		public int effect;
		public List<CardType> cardType;

		public enum CardRarity
		{
			Common,
			Uncommon,
			Rare,
			Epic,
			Legendary
		}

		public enum CardType
		{
			attack,
			heal,
			question
		}
	}
}


