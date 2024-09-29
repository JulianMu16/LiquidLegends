using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawController : Health
{
    public Transform player;          // Reference to the player character
    public Transform soaker;          // Reference to the enemy's gun
    public GameObject bubblePrefab;   // Bubble prefab to instantiate
    public float fireRate = 1.0f;     // How often the enemy shoots
    public float bubbleSpeed = 7f;    // Speed of the enemy's bubbles
    public float verticalMoveSpeed = 1f;  // Speed of the vertical movement
    public float verticalMoveDistance = 2f;  // Distance to move up and down
    private float nextFireTime = 0f;  // Timer to control fire rate
    private Vector3 initialPosition;  // Initial position to calculate vertical movement

    private void Start()
    {
        health = 5;
        characterType = 'b';
        initialPosition = transform.position;

        // Start the coroutine to move up and down
        StartCoroutine(MoveUpAndDown());
    }

    void Update()
    {
        HandleShooting();
    }

    // Handle enemy shooting logic
    void HandleShooting()
    {
        // If the current time is past the next allowed fire time
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            ShootAtPlayer();
        }
    }

    // Instantiate and shoot a bubble towards the player
    void ShootAtPlayer()
    {
        // Create the bubble at the soaker's position
        GameObject bubble = Instantiate(bubblePrefab, soaker.position, soaker.rotation);
        Physics2D.IgnoreCollision(bubble.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // Get the BubbleController script and initialize it with the direction
        BubbleController bubbleController = bubble.GetComponent<BubbleController>();
        Vector3 shootingDirection = (player.position - soaker.position).normalized;
        bubbleController.Initialize(shootingDirection);
        bubbleController.speed = bubbleSpeed;
    }

    // Coroutine to move up and down
    IEnumerator MoveUpAndDown()
    {
        while (true)
        {
            // Calculate the new position
            Vector3 targetPosition = initialPosition + Vector3.up * verticalMoveDistance;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, verticalMoveSpeed * Time.deltaTime);
                yield return null;  // Wait for the next frame
            }

            // Swap target position to initial position for downward movement
            targetPosition = initialPosition - Vector3.up * verticalMoveDistance;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, verticalMoveSpeed * Time.deltaTime);
                yield return null;  // Wait for the next frame
            }
        }
    }
}