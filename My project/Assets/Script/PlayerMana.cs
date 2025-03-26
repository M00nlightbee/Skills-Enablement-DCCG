using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour
{
    public Text PlayerManaText;
    public int currentMana = 3;
    public int maxMana = 10;
    public int cardCost = 3;

    public Image[] PlayerManaBlock;
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
        for (int i = 0; i < PlayerManaBlock.Length; i++)
        {
            if (i < currentMana)
            {
                PlayerManaBlock[i].color = fullColor;
            }
            else
            {
                PlayerManaBlock[i].color = emptyColor;
            }
        }

        if (PlayerManaText != null)
        {
            PlayerManaText.text = currentMana + "/" + maxMana;
        }
    }

    public void TryPlayCard()
    {
        if (currentMana >= cardCost)
        {
            UseMana(cardCost);
            Debug.Log("Player played a card!");
        }
        else
        {
            Debug.Log("Not enough mana.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryPlayCard(); // Tries to play a card
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            UseMana(1); // Uses 1 mana
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RestoreMana(1); // Restores 1 mana
        }
    }
}
