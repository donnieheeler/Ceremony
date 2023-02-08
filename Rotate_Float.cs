using UnityEngine;

public class Rotate_Float : MonoBehaviour
{
    // Floating and rotation parameters
    public float floatAmplitude = -0.07f;
    public float floatSpeed = 1f; 
    private float tempY;
    private Vector3 tempPosition;

    void Start()
    {
        // Sets the object's position as tempPosition, then set tempPosition's Y coordinate as tempY 
        tempPosition = transform.position;
        tempY = tempPosition.y;
    }

    void Update()
    {
        /*
        1. Rotates the object around the Y-axis by 25 degrees.
        2. Moves the object up and down using a sine wave.
        */

        //1.
        transform.Rotate(Vector3.up * Time.deltaTime * 25f, Space.World);

        //2.
        tempPosition.y = tempY + floatAmplitude * Mathf.Sin(floatSpeed * Time.time);
        transform.position = tempPosition;
    }
}