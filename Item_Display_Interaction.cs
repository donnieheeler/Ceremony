using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Display_Interaction : MonoBehaviour
{
    // Message parameters
    public string message;
    public Font font;
    public int fontSize = 75;
    public float xcord = 0f;
    public float ycord = 300f;
    public float duration = 1f;

    // Item parameters
    public GameObject willAppear;
    public GameObject willDisappear;

    // Overlay parameters
    public GameObject pickupOverlay;
    private Image pickupImage;
    private float currentAlpha = 0f;
    private float intargetAlpha = .2f;
    private float inalphaIncrement = .05f;
    private float outtargetAlpha = 0f;
    private float outalphaIncrement = .003f;

    // Connecting parameter
    public Display_Message_Anchor collisionScript;

    // Display time variables
    private float displayTime;
    private bool displayTimeSet = false;

    void Start()
    {
        // Grabs "Image" component from overlay gameObject
        pickupImage = pickupOverlay.GetComponent<Image>();
    }

    private void Update()
    {
        // Sets new "Alpha" amount every frame
        pickupImage.color = new Color(pickupImage.color.r, pickupImage.color.g, pickupImage.color.b, currentAlpha);
    }


    private void OnGUI()
    {
        // Checks if anchored object triggers collision
        if (collisionScript.collisionTriggered)
        {
            // Checks if displayTime has not been set
            if (!displayTimeSet)
            {
                /* 
                1. Sets the displayTime as the current time plus the set duration    
                2. Confirms that the displayTime has been set
                3. Starts FadeIn
                */

                //1.
                displayTime = Time.time + duration;

                //2.
                displayTimeSet = true;

                //3.
                StartCoroutine(FadeIn());
            }

            // Checks if the time is greater than the displayTime, as it grows the 
            if (Time.time < displayTime)
            {
                /* 
                1. Creates a text box / rect(angle) object at the set X and Y coordinates
                2. Creates and instance of GUIStyle which defines how GUI.Label will look
                3. Sets the desired font, font size, and color
                4. Center aligns the text
                5. Displays the message as defined by the previous parameters
                6. Activates and Deactivates set objects from scene
                */

                //1.
                Rect textRect = new Rect(xcord, ycord, Screen.width, Screen.height);

                //2.
                GUIStyle style = new GUIStyle();

                //3.
                style.font = font;
                style.fontSize = fontSize;
                style.normal.textColor = new Color32(252, 3, 3, 255);

                //4.
                style.alignment = TextAnchor.MiddleCenter;

                //5.
                GUI.Label(textRect, message, style);

                //6.
                willDisappear.SetActive(false);
                willAppear.SetActive(true);

            }

        }

    }

    private IEnumerator FadeIn()
    {
        while (currentAlpha < intargetAlpha)
        {
            // Adds the set fade in alpha increment to the current amount 
            currentAlpha += inalphaIncrement;
            pickupImage.color = new Color(pickupImage.color.r, pickupImage.color.g, pickupImage.color.b, currentAlpha);
            yield return null;
        }
            // Begins the fade out coroutine after reaching the set alpha max
        StartCoroutine(FadeOut());
        }

    private IEnumerator FadeOut()
    {
        while (currentAlpha > outtargetAlpha)
        {
            // Subtracts the set fade out alpha increment to the current amount
            currentAlpha -= outalphaIncrement;
            pickupImage.color = new Color(pickupImage.color.r, pickupImage.color.g, pickupImage.color.b, currentAlpha);
            yield return null;
        }
    }   
}