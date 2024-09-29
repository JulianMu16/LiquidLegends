using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float speed;  
    private Vector3 direction;

    // Size oscillation
    public float scaleMagnitude = 0.1f;
    public float oscillationSpeed = 2f;

    public void Initialize(Vector3 direction)
    {
        this.direction = direction.normalized;
    
        // Destroy the bubble after 8 seconds
        Destroy(gameObject, 8f); 
    }

    void Update()
    {
        // Move the bubble in the direction it was initialized
        transform.position += direction * speed * Time.deltaTime;

        // Oscillate the bullet's size
        float scale = 1 + Mathf.Sin(Time.time * oscillationSpeed) * scaleMagnitude;
        transform.localScale = new Vector3(scale, scale, 1); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
