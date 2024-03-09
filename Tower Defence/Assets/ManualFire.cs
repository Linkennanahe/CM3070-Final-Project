using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManualFire : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button is clicked and the cursor is not over a UI element
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            FireGun();
        }
    }

    void FireGun()
    {
        GameObject bulletObject = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);

        // Access the Bullet script on the instantiated bulletObject
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();

        // Check if the bulletScript is not null before using it
        if (bulletScript != null)
        {
            // You can now access methods or properties from the Bullet script
            Bullet.BulletType bulletType = bulletScript.GetBulletType();

            Debug.Log("Fired a bullet of type: " + bulletType.ToString());
        }

        Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * BulletSpeed, ForceMode2D.Impulse);
    }
}
