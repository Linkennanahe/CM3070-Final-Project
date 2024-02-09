using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    // Number of Gun points
    public Transform FirePoint1;
    public Transform FirePoint2;
    public GameObject Bullet;
    // Bullet Flying speed
    public float BulletSpeed = 10f;
    // Rate of fire
    public float fireRate = 1f; 
    // Time needed before next shot is fired
    private float nextFireTime = 0f;

    void Update()
    {
        
    }

    public void TriggerAutoFire()
    {
        if (Time.time >= nextFireTime)
        {
            FireGun(FirePoint1);
            FireGun(FirePoint2);
            // Inversely propotional to fireRate
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireGun(Transform firePoint)
    {
        // Bullet spawn at the fire point and inherit the specifications
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * BulletSpeed, ForceMode2D.Impulse);
    }
}
