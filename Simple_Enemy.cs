using UnityEngine;
using System.Collections;

public class Simple_Enemy : MonoBehaviour
{
    // Enemy parameters
    public float speed = 5f;
    public float health = 100f;
    public float visionRadius = 5f;
    public float yAxis;
    public GameObject enemy;
    public GameObject gibPrefab;
    public ParticleSystem bloodParticles;

    // Player information variables
    public GameObject player;
    private Transform playerTransform;
    private Vector3 initialPosition;
    private bool patrolXcord = true;

    private void Start()
    {
        // Assigns playerTransform 
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Assigns enemy's initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        // Creates target variable
        Vector3 target;

        // Checks if player is visible to enemy
        if (CanSeePlayer())
        {
            // Sets the target as the player's position
            target = playerTransform.position;

            // Makes the enemy face the player
            transform.LookAt(playerTransform);
        }
        else
        {
            // If the player is not visible to the enemy assigns "target" as the intial position of the enemy
            target = initialPosition + new Vector3((patrolXcord ? 1 : -1) * visionRadius, 0, 0);

            // Makes the enemy face the direction it is patrolling
            Quaternion rotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

        }

        // Moves the enemy towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Checks if enemy has reached the target position
        if (transform.position == target)
        {
            // Reverse patrol direction 
            patrolXcord = !patrolXcord;
        }

        // Casts ray down to detect ground and activates if ground is detected
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f))
        {
            // Sets the raycast discovered Y-cord to the player position
            yAxis = hit.point.y;
            transform.position = new Vector3(transform.position.x, yAxis, transform.position.z);
        }
    }

    // Damage and death controller
    public void TakeDamage(float damage)
    {
        // Subtracts damage from health
        health -= damage;

        // Checks if health is below 0
        if (health <= 0f)
        {
            // Deactivate enemy game object
            enemy.SetActive(false);

            // Activates gibSystem()
            gibSystem();
        }
    }

    // Creates gore giblets after enemy death
    public void gibSystem()
    {
        // Loops 3 times, creating 3 gibs and particle effects
        for (int i = 0; i < 3; i++)
        {
            // Instantiates gib
            GameObject gib = Instantiate(gibPrefab, enemy.transform.position + new Vector3(0, 1, 0), enemy.transform.rotation);

            // Calculates velocity of gib
            Vector3 velocity = enemy.transform.forward * -2;
            velocity += new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
            gib.GetComponent<Rigidbody>().velocity = velocity;

            // Instantiates blood particles
            ParticleSystem blood = Instantiate(bloodParticles, enemy.transform.position + new Vector3(0, 1, 0), enemy.transform.rotation);

            // Destroys clones
            Destroy(gib, 5f);
            Destroy(blood.gameObject, 3f);
        }
    }

    // Checks if enemy can see the player
    private bool CanSeePlayer()
    {
        // Calculates if the enemy is within the assigned visionRadius of the enemy 
        return Vector3.Distance(transform.position, playerTransform.position) < visionRadius && 
        Vector3.Angle(transform.forward, playerTransform.position - transform.position) < 90f;
    }
}
