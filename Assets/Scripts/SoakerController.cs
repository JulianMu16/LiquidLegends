using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoakerController : MonoBehaviour
{
    public Transform player;
    public Transform soaker;
    public GameObject bubblePrefab;

    SpriteRenderer spriteRenderer;
    Animator animator;

    // Distance of the gun from the player
    public float offsetDistance; 

    // Shooting mechanics
    public float fireRate;
    public float nextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateSoakerToMouse();
        HandleShooting();
    }

    // Have the gun track the mouse
    void RotateSoakerToMouse()
    {
        // Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
    
        // Calculate direction vector from player to mouse
        Vector3 direction = mousePosition - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Rotate the gun to face the mouse
        soaker.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Position the gun relative to the player at a fixed distance
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * offsetDistance;
        soaker.position = player.position + offset;
    }


    // Handle shooting logic
    void HandleShooting()
    {
        // "Fire1" is the left mouse button
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime) 
        {
            // Set the next fire time
            nextFireTime = Time.time + fireRate; 
            Shoot();
        }
    }


    // Create and shoot a bubble
    void Shoot()
    {
        // Activate shooting animation
        animator.SetTrigger("SoakerBlast");

        // Calculate the offset position for the bullet
        // Change 0.5f to the desired offset distance
        Vector3 offset = new Vector3(Mathf.Cos((soaker.rotation.eulerAngles.z) * Mathf.Deg2Rad), 
                                     Mathf.Sin((soaker.rotation.eulerAngles.z) * Mathf.Deg2Rad), 0) * 0.5f; 

        // Create bubble instance
        GameObject bubble = Instantiate(bubblePrefab, soaker.position + offset, soaker.rotation);

        // Get the bubble controller and initialize it with the direction
        BubbleController bubbleController = bubble.GetComponent<BubbleController>();

        // Bullet appears at the right end of the soaker
        Vector3 shootingDirection = soaker.right;
        bubbleController.Initialize(shootingDirection);
    }
}
