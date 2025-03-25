using UnityEngine;
using UnityEngine.UI;

public class AIManaBar : MonoBehaviour
{
    public Text AIManaText;
    public int currentMana = 3;
    public int maxMana = 10;
    public int cardCost = 3;

    public Image[] manaBlocks; // Mana block images
    public Color fullColor = Color.cyan;
    public Color emptyColor = Color.gray;

    void Start()
    {
        UpdateManaBar();
    }

    void Update()
    {
        // Test spending mana
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseMana(1);
        }

        // Test restoring mana
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreMana(1);
        }

        // Test AI playing card if enough mana
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPlayCard();
        }
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
            manaBlocks[i].color = i < currentMana ? fullColor : emptyColor;
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
            // You can add visual logic here later
        }
        else
        {
            Debug.Log("Not enough mana for AI to play.");
        }
    }
}
