using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunControl : MonoBehaviour
{
    // The by default is 90 degree Anticlockwise, so must -90 to make it point to the mouse
    public float offset = -90f; 

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
    }

    void PointToMouse()
    {
        // Get the mouse position in screen coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the current position to the mouse position
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // Calculate the MainGunAngle to rotate the object, including the offset
        float MainGunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

        // Rotate the MainGun towards the mouse position
        transform.rotation = Quaternion.AngleAxis(MainGunAngle, Vector3.forward);
    }
}
