using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shot_Engine : MonoBehaviour
{
    // Shot parameters
    public GameObject shotPrefab;
    public Transform shotSpawnPoint; 
    public GameObject itemRequired;
    public GameObject shotBlastEffect;
    public float shotSpeed = 50f;
    public float cooldownTime = 1f;
    private float currentCooldown = 0f;

    // Trail parameters
    public TrailRenderer trailRenderer;

    // Audio parameters
    public AudioClip sound;
    public AudioSource audioSource;

    // Light parameters
    public Light itemLight;

    // Particle system parameters
    public ParticleSystem Particles;

    // Player parameters
    public GameObject PlayerFollowCamera;
    private GameObject Capsule;


    private void Start()
    {
        // Initialize the audio source with the sound clip
        audioSource.clip = sound;
    }

    void Update()
    {
        // Checks if the player presses the "Fire1" button and if the current cooldown is 0
        if (Input.GetButtonDown("Fire1")  && currentCooldown <= 0)
        {
            // Checks if the required item is active in the hierarchy
            if (itemRequired.activeInHierarchy)
            {
                // Plays the sound using the audio source
                audioSource.PlayOneShot(sound);

                // Calls the function to shoot the shot
                ShootShot();

                // Sets the current cooldown to the assigned cooldown time
                currentCooldown = cooldownTime;
            }
        }

        // Starts lowering the current cooldown by the time passed in each frame
        currentCooldown -= Time.deltaTime;
    }

    void ShootShot()
    {
        // Instantiates a new shot and trail at the spawn point
        GameObject shot = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation);
        trailRenderer.emitting = true;

        // Sets the shot's direction and speed
        shot.GetComponent<Rigidbody>().velocity = shotSpawnPoint.forward * shotSpeed;

        // Destroys the shot after 1 second
        Destroy(shot, 1f);

        // Rotates the player capsule by 1.5 degrees in the x axis then -0.25 (Recoil)
        Capsule = GameObject.Find("PlayerCapsule/Capsule");
        Vector3 rotation = Capsule.transform.rotation.eulerAngles;
        rotation.x -= 1.5f;
        rotation.x += .25f;
        Capsule.transform.rotation = Quaternion.Euler(rotation);

        // Gets the virtual camera component and sets it's field of view to 71 degrees. (Power)
        CinemachineVirtualCamera virtualCamera = PlayerFollowCamera.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.FieldOfView = 71f;

        // Calls the ResetVerticalFOV method after .075 seconds
        Invoke("ResetVerticalFOV", .075f);

        // Starts the DropIntensity then RiseIntensity coroutines (Cooldown visualizer)
        StartCoroutine(DropIntensity());
        StartCoroutine(RiseIntensity());

        // Instantiates a particle system at the shot spawn point (Blast particles / sparks)
        ParticleSystem sparks = Instantiate(Particles, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);
        sparks.GetComponent<ParticleSystem>().transform.LookAt(transform.position);

        // Destroys the particle system after 1 second
        Destroy(sparks.gameObject, 1f);

        // Activate the shot blast effect and call the Deactivate method after .075 seconds (Blast lighting)
        shotBlastEffect.SetActive(true);
        Invoke("Deactivate", .075f);
    }


    // Deactivates blast lighting effect
    private void Deactivate()
    {
        shotBlastEffect.SetActive(false);
    }

    // Resets FOV (Power)
    void ResetVerticalFOV()
    {
        CinemachineVirtualCamera virtualCamera = PlayerFollowCamera.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.FieldOfView = 70f;
    }

    // Decreases item light (Cooldown visualizer)
    private IEnumerator DropIntensity()
    {
        // Time elapsed
        float t = 0f;

        // Duration of intensity transition
        float duration = 0.001f;

        // Starting intensity and ending intensity
        float startIntensity = itemLight.intensity;
        float endIntensity = 0f;

        // Checks if the time elapsed is less than the duration
        while (t < duration)
        {
            // Increments time elapsed
            t += Time.deltaTime;

            // Transitions the intensity of the light
            itemLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t / duration);

            // Waits for the next frame
            yield return null;
        }

        // Sets the final intensity
        itemLight.intensity = endIntensity;
    }

    // Increases item light (Cooldown visualizer)
    private IEnumerator RiseIntensity()
    {   
        // Time elapsed
        float t = 0f;

        // Duration of intensity transition
        float duration = cooldownTime;

        // Starting intensity and ending intensity
        float startIntensity = itemLight.intensity;
        float endIntensity = 5f;

        // Checks if the time elapsed is less than the duration
        while (t < duration)
        {
            // Increments time elapsed
            t += Time.deltaTime;

            // Transitions the intensity of the light
            itemLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t / duration);

            // Waits for the next frame
            yield return null;
        }

        // Sets the final intensity
        itemLight.intensity = endIntensity;
    }
}