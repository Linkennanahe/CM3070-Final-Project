using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLockOn : MonoBehaviour
{
    // Detection Radius
    public float detectionRadius = 3f;
    // The by default is 90 degree Anticlockwise, so must -90 to make it point to the enemy 
    public float offset = -90f; 
    private Transform EnemyTransform;

    private AutoFire autoFireScript;

    private void Start()
    {
        // Find the Enemy's Location
        EnemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

        // Access the AutoFire Script
        autoFireScript = GetComponent<AutoFire>();
    }


    void Update()
    {
        if (EnemyTransform != null)
        {
            // Calculate the direction to the enemy
            Vector3 direction = EnemyTransform.position - transform.position;

            // If enemy is in detection Radius
            if (direction.magnitude <= detectionRadius)
            {
                // Calculate the TurretAngle needed
                float TurretAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

                // Rotate the Turret towards the enemy
                transform.rotation = Quaternion.Euler(0f, 0f, TurretAngle);

                // Trigger FireGun in AutoFire script
                autoFireScript.TriggerAutoFire();
            }
            else
            {
                // Maybe can have a detection range so cannon rotates but dont fire until enemy enter fire range
            }
        }
    }
}
