using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

	// List to hold prefab
	public List<GameObject> cards = new List<GameObject>();

	// the starting position for the first card
	public Vector2 startPosition = new Vector2(-6, -3.25f);

	// horizontal spacing between cards
	public float spacing = 3.0f;

	void Start()
	{
		Spawn();
	}

	// Path to the CSV file containing card data
	public string csvFilePath = "csv file path";

	public HealthBar healthBar;

	void Spawn()
	{
		// Create an instance of the Card component
		Card cardComponent = new Card();
		Card.CardData[] cardDataArray = cardComponent.LoadCardDataFromCSV(csvFilePath);

		if (cardDataArray != null && cardDataArray.Length > 0)
		{
			// Loop through the list and spawn each card at an offset based on its index
			for (int i = 0; i < cards.Count; i++)
			{
				if (cards[i] != null)
				{
					Vector2 spawnPosition = startPosition + new Vector2(spacing * i, 0);
					GameObject cardObject = Instantiate(cards[i], spawnPosition, Quaternion.identity);

					// Randomly select a card from the array
					int randomIndex = Random.Range(0, cardDataArray.Length);
					Card.CardData randomCard = cardDataArray[randomIndex];

					// Update the TextMesh components with the random card data
					Card cardScript = cardObject.GetComponent<Card>();
					if (cardScript != null)
					{
						cardScript.nameTextMesh.text = randomCard.card_name;
						cardScript.typeTextMesh.text = randomCard.card_type;
						cardScript.mechsTextMesh.text = randomCard.mechanics;
						cardScript.rarityTextMesh.text = randomCard.rarity;
						cardScript.damageTextMesh.text = randomCard.damage.ToString();
					}

					// Assign the HealthBar reference to the DragObject component
					DragObject dragObject = cardObject.GetComponent<DragObject>();
					if (dragObject != null)
					{
						dragObject.healthBar = healthBar;
					}
				}
			}
		}
	}
}
