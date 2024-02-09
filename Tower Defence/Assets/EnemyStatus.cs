using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // List of status available
    private enum EnemyStatusType
    {
        Normal,
        Burning,
        Wet
    }

    // Set Default status to Normal
    private EnemyStatusType currentStatus = EnemyStatusType.Normal;

    void Start()
    {

    }

    void Update()
    {
        // Different Enemy Status future implement reaction based on status of enemy
        switch (currentStatus)
        {
            // Standard
            case EnemyStatusType.Normal:
                break;
                //Burning
            case EnemyStatusType.Burning:
                Debug.Log("Enemy is burning!");
                break;
                //Wet
            case EnemyStatusType.Wet:
                Debug.Log("Enemy is wet!");
                break;
        }

        Debug.Log("Current Status: " + currentStatus.ToString());
    }


    void ApplyBurningStatus()
    {
        // Apply burning status
        currentStatus = EnemyStatusType.Burning;
    }

    void ApplyWetStatus()
    {
        // Apply wet status
        currentStatus = EnemyStatusType.Wet;
    }
}
