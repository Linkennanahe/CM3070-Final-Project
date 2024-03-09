using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Physical,
        Fire,
        Water
    }

    public BulletType bulletType = BulletType.Physical;

    // Allow manual selection of sprite for different bullet
    public Sprite physicalSprite;
    public Sprite fireSprite;
    public Sprite waterSprite;

    private SpriteRenderer spriteRenderer;
    // Damage for each type of bullet
    public float physicalBulletDamage = 10f;
    private float fireBulletDamage;
    private float waterBulletDamage;

    private float bulletDamage; // Current damage of the bullet

    

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        fireBulletDamage = 2f*physicalBulletDamage;
        waterBulletDamage = 0.5f*physicalBulletDamage;

        // Change Sprite based on bullet type
        SetSprite();

        // Set bullet damage based on bullet type
        SetBulletDamage();

        // Destroy the bullet after 2 seconds
        Destroy(gameObject, 2f);
    }

    void SetSprite()
    {
        switch (bulletType)
        {
            case BulletType.Physical:
                spriteRenderer.sprite = physicalSprite;
                break;
            case BulletType.Fire:
                spriteRenderer.sprite = fireSprite;
                break;
            case BulletType.Water:
                spriteRenderer.sprite = waterSprite;
                break;
        }
    }

    void SetBulletDamage()
    {
        switch (bulletType)
        {
            case BulletType.Physical:
                bulletDamage = physicalBulletDamage;
                break;
            case BulletType.Fire:
                bulletDamage = fireBulletDamage;
                break;
            case BulletType.Water:
                bulletDamage = waterBulletDamage;
                break;
        }
    }

    public BulletType GetBulletType()
    {
        return bulletType;
    }

    public float GetBulletDamage()
    {
        return bulletDamage;
    }

    public void ChangeBulletType(BulletType newBulletType)
    {
        bulletType = newBulletType;
        SetSprite(); // Update the sprite immediately when changing bullet type
        SetBulletDamage(); // Update the bullet damage immediately when changing bullet type
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the bullet on collision with an enemy
            Destroy(gameObject);
        }
        // Check if the collision is with obstacles
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            // Destroy the bullet on collision with obstacles
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Console log to see if it changes
        Debug.Log("Bullet Type: " + bulletType.ToString() + ", Bullet Damage: " + bulletDamage.ToString());
    }
}
