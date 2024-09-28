using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawSoakerController : MonoBehaviour
{
    public Transform outlaw;
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
        RotateSoakerTowardsPlayer();
    }


    // Rotate the enemy's soaker towards the player
    void RotateSoakerTowardsPlayer()
    {
        // Get the player's position
        Vector3 playerPosition = player.position;
        playerPosition.z = 0;  

        // Calculate the direction vector from the enemy to the player
        Vector3 direction = playerPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the enemy's soaker to face the player
        soaker.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Position the gun relative to the outlaw at a fixed distance
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * offsetDistance;
        soaker.position = outlaw.position + offset;
    }
}
