using UnityEngine;
using UnityEngine.UI;

public class AIManaBar : MonoBehaviour
{
    public Text AIManaText;
    public int currentMana = 3;
    public int maxMana = 10;
    public int cardCost = 3;

    public Image[] manaBlocks;
    public Color fullColor = Color.cyan;
    public Color emptyColor = Color.gray;

    void Start()
    {
        UpdateManaBar();
    }

    public void UseMana(int amount)
    {
        currentMana = Mathf.Max(0, currentMana - amount);
        UpdateManaBar();
    }

    public void RestoreMana(int amount)
    {
        currentMana = Mathf.Min(maxMana, currentMana + amount);
        UpdateManaBar();
    }

    void UpdateManaBar()
    {
        for (int i = 0; i < manaBlocks.Length; i++)
        {
            if (i < currentMana)
            {
                manaBlocks[i].color = fullColor;
            }
            else
            {
                manaBlocks[i].color = emptyColor;
            }
        }

        if (AIManaText != null)
        {
            AIManaText.text = currentMana + "/" + maxMana;
        }
    }

    public void TryPlayCard()
    {
        if (currentMana >= cardCost)
        {
            UseMana(cardCost);
            Debug.Log("AI played a card!");
        }
        else
        {
            Debug.Log("Not enough mana.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPlayCard(); // Tries to play a card
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseMana(1); // Uses 1 mana
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreMana(1); // Restores 1 mana
        }
    }
}
