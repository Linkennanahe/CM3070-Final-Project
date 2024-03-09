using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTypeChanger : MonoBehaviour
{
    public Bullet bullet; // Reference to the Bullet script

    public void ChangeBulletTypeToPhysical()
    {
        if (bullet != null)
        {
            Debug.Log("Changing bullet type to Physical");
            bullet.ChangeBulletType(Bullet.BulletType.Physical);
        }
    }

    public void ChangeBulletTypeToFire()
    {
        if (bullet != null)
        {
            Debug.Log("Changing bullet type to Fire");
            bullet.ChangeBulletType(Bullet.BulletType.Fire);
        }
    }

    public void ChangeBulletTypeToWater()
    {
        if (bullet != null)
        {
            Debug.Log("Changing bullet type to Water");
            bullet.ChangeBulletType(Bullet.BulletType.Water);
        }
    }
}
