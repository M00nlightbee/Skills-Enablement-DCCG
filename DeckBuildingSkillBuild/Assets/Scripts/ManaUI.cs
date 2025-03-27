using UnityEngine;
using TMPro;

public class ManaUI : MonoBehaviour
{
	public TMP_Text manaText;

	public void UpdateManaUI(int mana)
	{
		manaText.text = $"{mana.ToString()} / 10 ";
	}
}

