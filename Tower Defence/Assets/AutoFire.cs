using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFire : MonoBehaviour
{
    // Number of Gun points
    public Transform FirePoint1;
    public Transform FirePoint2;
    public GameObject Bullet;
    // Bullet Flying speed
    public float BulletSpeed = 10f;
    // Base rate of fire
    public float baseFireRate = 1f;
    // Current rate of fire
    private float currentFireRate;
    // Maximum fire rate
    public float maxFireRate = 10f;
    // Time needed before next shot is fired
    private float nextFireTime = 0f;

    // Reference to the UI button
    public Button increaseFireRateButton;

    void Start()
    {
        // Initialize the current fire rate
        currentFireRate = baseFireRate;

        // Add a listener to the UI button to call IncreaseFireRate when clicked
        increaseFireRateButton.onClick.AddListener(IncreaseFireRate);
    }

    void Update()
    {

    }

    public void TriggerAutoFire()
    {
        if (Time.time >= nextFireTime)
        {
            FireGun(FirePoint1);
            FireGun(FirePoint2);
            // Inversely proportional to current fire rate
            nextFireTime = Time.time + 1f / currentFireRate;
        }
    }

    void FireGun(Transform firePoint)
    {
        // Bullet spawn at the fire point and inherit the specifications
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * BulletSpeed, ForceMode2D.Impulse);
    }

    void IncreaseFireRate()
    {
        // Increase the fire rate
        currentFireRate *= 1.5f;

        // fire rate cannot exceed maximum value
        currentFireRate = Mathf.Clamp(currentFireRate, baseFireRate, maxFireRate);

        Debug.Log("Fire Rate Increased: " + currentFireRate);
    }
}
