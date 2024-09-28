using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtain speed from player input
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;

        // Update rigidbody with velocity vector
        rb.velocity = new Vector2(speedX, speedY);

        // Update animator
        animator.SetFloat("Velocity", Math.Abs(speedX + speedY));
        
        // Flip sprite in the direction player is moving
        if (speedX < 0)
        {
            spriteRenderer.flipX = true;  // Flip to the left
        }
        else if (speedX > 0)
        {
            spriteRenderer.flipX = false; // Flip to the right
        }

    }
}
