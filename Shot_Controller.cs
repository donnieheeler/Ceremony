using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Controller : MonoBehaviour
{
    // Particle system parameters
    public ParticleSystem Particles;

    // Damage amount parameters
    public float damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Assigns the simple enemy component to simpleEnemyComponent
        Simple_Enemy simpleEnemyComponent = other.gameObject.GetComponent<Simple_Enemy>();

        // Checks if simple enemy controller has been assigned
        if (simpleEnemyComponent != null)
        {
            // Calls "TakeDamage" method in the simple enemy component
            simpleEnemyComponent.TakeDamage(damageAmount);
        }

        // Destroys the shot
        Destroy(gameObject);

        // Creates particle effect at the position and rotation of the shot
        ParticleSystem sparks = Instantiate(Particles, gameObject.transform.position, gameObject.transform.rotation);

        // Faces the particle effect in the opposite direction of the shot's position
        sparks.GetComponent<ParticleSystem>().transform.LookAt(transform.position * -1);

        // Destroys the particle effect after 1 second
        Destroy(sparks.gameObject, 1f);
    }
}