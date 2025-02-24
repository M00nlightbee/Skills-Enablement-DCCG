using System.IO;
using UnityEngine;

public class Card : MonoBehaviour
{
	// Define a serializable class for card data.
	[System.Serializable]
	public class CardData
	{
		public string card_name;
		public string card_type;
		public string mechanics;
		public string rarity;
		public int damage;
	}

	// TextMesh components for displaying card data
	public TextMesh nameTextMesh;
	public TextMesh typeTextMesh;
	public TextMesh mechsTextMesh;
	public TextMesh rarityTextMesh;
	public TextMesh damageTextMesh;

	// The file name where the card data will be saved.
	private string fileName = "cardData.json";

	public void SaveCardData(CardData card)
	{
		// Convert the CardData object to a JSON string (pretty printed).
		string json = JsonUtility.ToJson(card, true);

		// Create the full file path.
		string filePath = Path.Combine(Application.persistentDataPath, fileName);

		// Write the JSON string to the file.
		File.WriteAllText(filePath, json);

		Debug.Log("Card data saved to " + filePath);
		Debug.Log("JSON Data:\n" + json);
	}

	public CardData LoadCardData()
	{
		string filePath = Path.Combine(Application.persistentDataPath, fileName);

		if (!File.Exists(filePath))
		{
			Debug.LogError("File not found: " + filePath);
			return null;
		}

		// Read the JSON string from the file.
		string json = File.ReadAllText(filePath);

		// Deserialize the JSON back into a CardData object.
		CardData cardData = JsonUtility.FromJson<CardData>(json);

		Debug.Log("Card data loaded from " + filePath);
		return cardData;
	}

	// Method to read and parse CSV file
	public CardData[] LoadCardDataFromCSV(string csvFilePath)
	{
		if (!File.Exists(csvFilePath))
		{
			Debug.LogError("CSV file not found: " + csvFilePath);
			return null;
		}

		string[] lines = File.ReadAllLines(csvFilePath);
		CardData[] cardDataArray = new CardData[lines.Length - 1];

		for (int i = 1; i < lines.Length; i++)
		{
			string[] fields = lines[i].Split(',');

			CardData cardData = new CardData
			{
				card_name = fields[0],
				card_type = fields[1],
				mechanics = fields[2],
				rarity = fields[3],
				damage = int.Parse(fields[4])
			};

			cardDataArray[i - 1] = cardData;
		}

		return cardDataArray;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// Filepath
		string csvFilePath = "own file path";

		CardData[] cardDataArray = LoadCardDataFromCSV(csvFilePath);

		if (cardDataArray != null && cardDataArray.Length > 0)
		{
			// Randomly select a card from the array
			int randomIndex = Random.Range(0, cardDataArray.Length);
			CardData randomCard = cardDataArray[randomIndex];

			SaveCardData(randomCard);
		}
	}

	// Update is called once per frame
	//void Update()
	//{
	//	// Press L to load the card from the JSON file.
	//	if (Input.GetKeyDown(KeyCode.L))
	//	{
	//		CardData loadedCard = LoadCardData();

	//		if (loadedCard != null)
	//		{
	//			Debug.Log("Loaded Card Details:");
	//			Debug.Log("Name: " + loadedCard.card_name);
	//			Debug.Log("Type: " + loadedCard.card_type);
	//			Debug.Log("Mechanics: " + loadedCard.mechanics);
	//			Debug.Log("Rarity: " + loadedCard.rarity);
	//			Debug.Log("Damage: " + loadedCard.damage);

	//			// Update the TextMesh components with the loaded card data
	//			nameTextMesh.text = loadedCard.card_name;
	//			typeTextMesh.text = loadedCard.card_type;
	//			mechsTextMesh.text = loadedCard.mechanics;
	//			rarityTextMesh.text = loadedCard.rarity;
	//			damageTextMesh.text = loadedCard.damage.ToString();

	//		}
	//	}
	//}
}
