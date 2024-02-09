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

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Change Sprite based on bullet type
        SetSprite();
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

    public BulletType GetBulletType()
    {
        return bulletType;
    }

    void Update()
    {
        // Console log to see if it changes
        Debug.Log("Bullet Type: " + bulletType.ToString());
    }
}
