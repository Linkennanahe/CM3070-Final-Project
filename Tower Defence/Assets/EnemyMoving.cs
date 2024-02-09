using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    //Movement speed of enemy
    public float moveSpeed = 3f;
    private bool isMoving = false;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            // Flip the sprite when moving left
            spriteRenderer.flipX = movement.x < 0;
        }
    }

    Vector2 movement;
    // Movement of enemy
    void Move()
    {   // Using keyboard for now for testing, will change to auto in future version
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();

        if (movement != Vector2.zero)
        {
            isMoving = true;
            animator.SetBool("isMoving", true);

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + new Vector3(movement.x, movement.y, 0f) * moveSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
        else
        {
            isMoving = false;
        }
    }
}
