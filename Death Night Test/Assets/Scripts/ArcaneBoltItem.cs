using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Arcane bolt class
public class ArcaneBoltItem : MonoBehaviour
{
    // This method is triggered when hits collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the game object that collided with the ArcaneBoltItem is tagged as "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, set the player's hasArcaneBolt property to true
            other.gameObject.GetComponent<PlayerControl>().hasArcaneBolt = true;

            // Destroy the ArcaneBoltItem game object
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    
    void Update()
    {
        
    }
}