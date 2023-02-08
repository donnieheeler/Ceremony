using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDI_Anchor : MonoBehaviour
{
    // Collision variable
    public bool collisionTriggered = false;

    // Runs if a collider enters the anchored object's collider    
    private void OnTriggerEnter(Collider other) 
    {
        // Checks if the collider is the player
        if (other.gameObject.tag == "Player")
        {
            // Confirms collision
            collisionTriggered = true;

            //Debug.Log(gameObject.name + " collided with: " + other.gameObject.name);
        }
    }

    // Runs when a collider leaves the anchored object's collider
    private void OnTriggerExit(Collider other)
    {
        // Checks if the collider is the player
        if (other.gameObject.tag == "Player")
        {
            // Confirms collision is no longer happening
            collisionTriggered = false;
        }
    }
}