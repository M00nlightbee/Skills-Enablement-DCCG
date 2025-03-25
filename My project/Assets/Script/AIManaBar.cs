using UnityEngine;
using UnityEngine.UI;


public class AIManaBar : MonoBehaviour
{
    public Text AIManaText;
    public int currentMana = 3;
    public int maxMana = 10;

    public Image[] manaBlocks; // Array of 10 mana images
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
            manaBlocks[i].color = i < currentMana ? fullColor : emptyColor;
        }
    }
    void Update()
{
    // Press Space to spend 1 mana
    if (Input.GetKeyDown(KeyCode.Space))
    {
        UseMana(1);
    }

    // Press R to restore 1 mana
    if (Input.GetKeyDown(KeyCode.R))
    {
        RestoreMana(1);
    }
    void UpdateManaBar()
    {
        // Update the blocks visually
        for (int i = 0; i < manaBlocks.Length; i++)
        {
            manaBlocks[i].color = i < currentMana ? fullColor : emptyColor;
        }

        // Update the "X/10" text
        if (AIManaText != null)
        {
            AIManaText.text = currentMana + "/" + maxMana;
        }
    }
    void Start()
{
    AIManaText.text = "It works!";
    UpdateManaBar();
}
}
}

