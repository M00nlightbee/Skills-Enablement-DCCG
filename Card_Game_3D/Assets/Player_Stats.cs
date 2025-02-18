using UnityEngine;

public class Player_Stats : MonoBehaviour
{

    public int health;
    public int max_health;
    public int shield;
    public int max_shield;
    public int mana;
    public int max_mana;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health>= max_health)
        {
            health = max_health;
        }
        if(shield>= max_shield)
        {
            shield = max_shield;
        }
        if(mana>= max_mana)
        {
            mana = max_mana;
        }
    }
}
