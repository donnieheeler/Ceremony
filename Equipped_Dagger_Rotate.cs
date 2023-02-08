using UnityEngine;

public class Equipped_Dagger_Rotate : MonoBehaviour
{
    // Rotation speed variable
    public float rotationSpeed = -150f;

    void Update()
    {
        /* 
        Rotates the dagger around the Z axis based on the set rotation speed multipled by the time 
        it took for the last frame to finish; ensuring consistent rotation speed regardless of frame rate.
        */
        
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}