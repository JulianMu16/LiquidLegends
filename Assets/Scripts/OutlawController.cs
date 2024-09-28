using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawController : MonoBehaviour
{
    public Transform player;          // Reference to the player character
    public Transform soaker;          // Reference to the enemy's gun
    public GameObject bubblePrefab;   // Bubble prefab to instantiate
    public float fireRate = 1.0f;     // How often the enemy shoots
    public float bubbleSpeed = 7f;    // Speed of the enemy's bubbles
    private float nextFireTime = 0f;  // Timer to control fire rate

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

        // Get the BubbleController script and initialize it with the direction
        BubbleController bubbleController = bubble.GetComponent<BubbleController>();
        Vector3 shootingDirection = (player.position - soaker.position).normalized;
        bubbleController.Initialize(shootingDirection);
        bubbleController.speed = bubbleSpeed; 
    }
}

