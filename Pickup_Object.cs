using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class Pickup_Object : MonoBehaviour
{
    // Player reach parameters
    public float reachDistance = 2f;
    public LayerMask pickupLayer;

    // Player parameters
    public GameObject disableWhileHolding;
    public Transform heldObjectHolder;

    // Pickup object parameters
    public GameObject heldObject;

    // Text parameter
    public Text pickupText;

    void Update()
    {
        // Check if Escape (Pause) key has been pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Checks if object is being held and drops it if yes
            if(heldObject != null)
            {
                Drop();
            }
        }

        // Checks if raycast has hit something and if another object is already held
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance, pickupLayer) && heldObject == null)
        {
            // Assigns object hit by raycast to item variable
            GameObject item = hit.collider.gameObject;

            // Checks if item has "PUObject" tag 
            if (item.tag == "PUObject")
            {
                // Displays item pickup UI
                pickupText.text = "Pick Up " + item.name;
                pickupText.gameObject.SetActive(true);
            }
        }
        else
        {
            // Disables item pickup UI if raycast isn't hitting any objects
            pickupText.gameObject.SetActive(false);
        }

        // Checks if E key is being pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Checks if object is being held
            if (heldObject == null)
            {
                Pickup();
            }
            else
            {
                Drop();
            }
        }

        // Checks if object is being held
        if (heldObject != null)
        {
            // Checks if right mouse button is being held
            if (Input.GetMouseButton(1))
            {
                // Checks if the player is on the ground
                if (GetComponent<FirstPersonController>().Grounded)
                {
                    // Calculates the rotation of the object along the Y-cord and X-cord based on the mouse movement
                    float rotX = Input.GetAxis("Mouse X") * Time.deltaTime * 500f;
                    float rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * 500f;

                    // Applies the rotation to the held object
                    heldObject.transform.Rotate(Vector3.up, -rotX);
                    heldObject.transform.Rotate(Vector3.right, rotY);

                    // Disables the player controller
                    GetComponent<FirstPersonController>().enabled = false;
                }
            }
            else
            {
                // Enables the player controller
                GetComponent<FirstPersonController>().enabled = true;
            }
        }
    }

    void Pickup()
    {
        // Checks if raycast has hit an object
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance, pickupLayer))
        {
            // Assigns object hit by raycast to item variable
            GameObject item = hit.collider.gameObject;

            // Checks if item has "PUObject" tag 
            if(item.tag == "PUObject")
            {
                // Assigns item name to heldObject variable
                heldObject = item;

                // Sets item as child of assigned heldObjectHolder 
                item.transform.SetParent(heldObjectHolder);

                // Makes item kinematic
                item.GetComponent<Rigidbody>().isKinematic = true;

                // Disables desired objects
                disableWhileHolding.SetActive(false);
            }
        }
    }

    void Drop()
    {
        // Removes item as child of assigned heldObjectHolder
        heldObject.transform.SetParent(null);

        // Makes object no longer kinematic
        heldObject.GetComponent<Rigidbody>().isKinematic = false;

        // Empties heldObject variable
        heldObject = null;

        // Reenables disabledd objects
        disableWhileHolding.SetActive(true);
    }
}