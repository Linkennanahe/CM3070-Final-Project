using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private enum EnemyStatusType
    {
        Normal,
        Burning,
        Wet
    }

    private EnemyStatusType currentStatus = EnemyStatusType.Normal;

    public float health = 100f;

    // Duration of each status in seconds
    private float burningDuration = 3f;
    private float wetDuration = 3f;

    // Timers for each status
    private float burningTimer = 0f;
    private float wetTimer = 0f;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Default color of the enemy
    private Color normalColor;

    // Color when the enemy is wet
    public Color wetColor = Color.blue;
    public Color burningColor = Color.red;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the default color of the enemy
        normalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Update status timers
        UpdateTimers();

        // Different Enemy Status, implement reaction based on current status of enemy
        switch (currentStatus)
        {
            case EnemyStatusType.Normal:
                // Restore the default color when the enemy is in the normal state
                spriteRenderer.color = normalColor;
                break;
            case EnemyStatusType.Burning:
                // Change the color when the enemy is burning
                spriteRenderer.color = burningColor;
                Debug.Log("Enemy is burning!");
                break;
            case EnemyStatusType.Wet:
                // Change the color when the enemy is wet
                spriteRenderer.color = wetColor;
                Debug.Log("Enemy is wet!");
                break;
        }

        Debug.Log("Current Status: " + currentStatus.ToString());
    }

    void ApplyBurningStatus()
    {
        // Apply burning status
        currentStatus = EnemyStatusType.Burning;
        // Set the timer to the duration
        burningTimer = burningDuration; 
    }

    void ApplyWetStatus()
    {
        // Apply wet status
        currentStatus = EnemyStatusType.Wet;
        // Set the timer to the duration
        wetTimer = wetDuration; 
    }

    void UpdateTimers()
    {
        // Update burning timer
        if (currentStatus == EnemyStatusType.Burning)
        {
            burningTimer -= Time.deltaTime;
            if (burningTimer <= 0)
            {
                // If the timer reaches 0, remove the burning status
                currentStatus = EnemyStatusType.Normal;
            }
        }

        // Update wet timer
        if (currentStatus == EnemyStatusType.Wet)
        {
            wetTimer -= Time.deltaTime;
            if (wetTimer <= 0)
            {
                // If the timer reaches 0, remove the wet status
                currentStatus = EnemyStatusType.Normal;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                // Check the bullet type and apply corresponding status
                switch (bullet.GetBulletType())
                {
                    case Bullet.BulletType.Physical:
                        // Deduct bullet damage from enemy health for physical bullets
                        health -= bullet.GetBulletDamage();
                        break;
                    case Bullet.BulletType.Fire:
                        // Apply burning status and calculate damage for fire bullets
                        if (currentStatus == EnemyStatusType.Wet)
                        {
                            health -= bullet.GetBulletDamage() * 5;
                        }
                        else
                        {
                            health -= bullet.GetBulletDamage();
                        }
                        currentStatus = EnemyStatusType.Normal;
                        ApplyBurningStatus();
                        break;
                    case Bullet.BulletType.Water:
                        // Apply wet status and calculate damage for water bullets
                        if (currentStatus == EnemyStatusType.Burning)
                        {
                            health -= bullet.GetBulletDamage() * 3;
                        }
                        else
                        {
                            health -= bullet.GetBulletDamage();
                        }
                        currentStatus = EnemyStatusType.Normal;
                        ApplyWetStatus();
                        break;
                }

                // Destroy the bullet after applying status
                Destroy(collision.gameObject);

                // Check if the enemy is destroyed
                if (health <= 0)
                {
                    // Increment the score and collect coins
                    GameManager.Instance.AddScore(1);
                    GameManager.Instance.AddCoins(1);

                    // Destroy the enemy
                    Destroy(gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the enemy on collision with player
            Destroy(gameObject);
        }
    }
}
