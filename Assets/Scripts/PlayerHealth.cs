using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount){
        currentHealth -= amount;

        if (currentHealth <= 0){
            Die();
        }
    }
    void Die(){
        Debug.Log("ded");
    }
}
