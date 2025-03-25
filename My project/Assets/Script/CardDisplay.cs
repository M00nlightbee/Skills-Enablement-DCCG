using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
	public Text nameText;
	public Text manaCostText;
	public Text attackText;
	public Text effectText;

	public void SetupCard(Card card)
	{
		nameText.text = card.cardName;
		manaCostText.text = card.manaCost.ToString();
		attackText.text = card.attack.ToString();
		effectText.text = card.effect;
	}
}
