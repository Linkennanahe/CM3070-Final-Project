using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretLockOn : MonoBehaviour
{
    public float detectionRadius = 3f;
    public float shootingRadius = 2f; // Added shooting radius
    public float offset = -90f;
    public float rotationSpeed = 180f;

    private Transform nearestEnemyTransform;
    private AutoFire autoFireScript;

    // Time interval for updating the enemy reference
    public float updateEnemyInterval = 3f;

    private void Start()
    {
        // Initial enemy reference update
        UpdateNearestEnemyReference();

        // Access the AutoFire Script
        autoFireScript = GetComponent<AutoFire>();

        // Start a coroutine to update the enemy reference at intervals
        StartCoroutine(UpdateNearestEnemyReferenceCoroutine());
    }

    void Update()
    {
        // Check if the enemy reference is not null
        if (nearestEnemyTransform != null)
        {
            Vector3 direction = nearestEnemyTransform.position - transform.position;

            // If enemy is in detection Radius
            if (direction.magnitude <= detectionRadius)
            {
                float turretAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

                Quaternion desiredRotation = Quaternion.Euler(0f, 0f, turretAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

                // Check if the enemy is within shooting radius
                if (direction.magnitude <= shootingRadius)
                {
                    autoFireScript.TriggerAutoFire();
                }
            }
            else
            {
                // You might want to add logic here for a detection range
            }
        }
    }

    void UpdateNearestEnemyReference()
    {
        // Find all potential enemies within the detection radius
        GameObject[] potentialEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemyObject in potentialEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemyObject.transform;
            }
        }

        nearestEnemyTransform = nearestEnemy;
    }

    IEnumerator UpdateNearestEnemyReferenceCoroutine()
    {
        while (true)
        {
            // Update the nearest enemy reference at intervals
            UpdateNearestEnemyReference();

            // Wait for the specified interval before updating again
            yield return new WaitForSeconds(updateEnemyInterval);
        }
    }
}
