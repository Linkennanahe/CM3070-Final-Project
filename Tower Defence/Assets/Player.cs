using UnityEngine;

public class Player : MonoBehaviour
{
    // Player attributes
    public int maxHealth = 100;
    public int currentHealth;
    public int score = 0;
    public int coins = 0;

    // Events for health, score, and coins changes
    public delegate void HealthChangedDelegate(int newHealth);
    public delegate void ScoreChangedDelegate(int newScore);
    public delegate void CoinsChangedDelegate(int newCoins);

    public event HealthChangedDelegate OnHealthChanged;
    public event ScoreChangedDelegate OnScoreChanged;
    public event CoinsChangedDelegate OnCoinsChanged;

    void Start()
    {
        // Initialize player attributes
        currentHealth = maxHealth;

        // Invoke events to notify initial values
        OnHealthChanged?.Invoke(currentHealth);
        OnScoreChanged?.Invoke(score);
        OnCoinsChanged?.Invoke(coins);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle enemy collision
            HandleEnemyCollision(collision.gameObject);
        }
    }

    void HandleEnemyCollision(GameObject enemy)
    {
        // Take damage when colliding with an enemy
        TakeDamage(1);

    }

    // Method to handle damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Invoke the health changed event
        OnHealthChanged?.Invoke(currentHealth);

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    // Method to handle gaining score
    public void GainScore(int points)
    {
        score += points;

        // Invoke the score changed event
        OnScoreChanged?.Invoke(score);
    }

    // Method to handle collecting coins
    public void CollectCoin(int coinValue)
    {
        coins += coinValue;

        // Invoke the coins changed event
        OnCoinsChanged?.Invoke(coins);
    }

    // Method to handle player defeat (customize as needed)
    void Defeat()
    {
        Debug.Log("Player Defeated!");
        // Implement your defeat logic here
    }
}
