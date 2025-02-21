using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	//// The prefab to spawn (assign this in the Inspector)
	//public GameObject prefab;

	//// Number of prefabs to spawn
	//public int numberOfPrefabs = 4;

	//// The space between each spawned prefab
	//public float spacing = 2.0f;

	//// The starting position for the first prefab
	//public Vector2 startPosition = new Vector2(-6, -3.25f);

	//void Start()
	//{
	//	Spawn();
	//}

	//void Spawn()
	//{
	//	for (int i = 0; i < numberOfPrefabs; i++)
	//	{
	//		// Calculate the position for each prefab. Here, we're spacing them out horizontally.
	//		Vector2 spawnPosition = startPosition + new Vector2(spacing * i, 0);

	//		// Instantiate the prefab at the computed position with no rotation
	//		Instantiate(prefab, spawnPosition, Quaternion.identity);
	//	}
	//}

	// List to hold your card prefabs. Assign these in the Inspector.
	public List<GameObject> cards = new List<GameObject>();

	// Define the starting position for the first card
	public Vector2 startPosition = new Vector2(-6, -3.25f);

	// Define the horizontal spacing between cards
	public float spacing = 3.0f;

	void Start()
	{
		Spawn();
	}

	//void Spawn()
	//{
	//	// Loop through the list and spawn each card at an offset based on its index
	//	for (int i = 0; i < cards.Count; i++)
	//	{
	//		if (cards[i] != null)
	//		{
	//			Vector2 spawnPosition = startPosition + new Vector2(spacing * i, 0);
	//			Instantiate(cards[i], spawnPosition, Quaternion.identity);
	//		}
	//	}
	//}

	// Path to the CSV file containing card data
	public string csvFilePath = "C:/Users/ouloy/Desktop/Create with Code - Blessing/2D-Prototype/cardData.csv";

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
