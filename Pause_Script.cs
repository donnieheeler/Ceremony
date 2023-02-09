using StarterAssets;
using UnityEngine;

public class Pause_Script : MonoBehaviour
{
    // Paused variable
    private bool isPaused = false;

    // Player control variables
    private FirstPersonController playerController;
    public GameObject shotManager;
    public GameObject playerCamera;
    public GameObject otherUI;


    // Font parameters
    public Font font;
    private int fontSize = 200;

    // Pause menu variable
    public GameObject pauseMenu;

    // Pause menu sound effects
    public AudioSource pauseSound;
    public AudioSource unpauseSound;

    private void Start()
    {
        // Set First Person Controller component to playerController variable
        playerController = GameObject.Find("PlayerCapsule").GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        // Stop time
        Time.timeScale = 0;

        // Play pause sound effect
        pauseSound.Play();

        // Disable player movement, shooting, and UI
        playerCamera.GetComponent<FirstPersonController>().enabled = false;
        playerController.enabled = false;
        shotManager.SetActive(false);
        otherUI.SetActive(false);

        // Enable cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //  Enable pause menu
        pauseMenu.SetActive(true);
    }

    private void Unpause()
    {
        // Start time
        Time.timeScale = 1;

        // Play unpause sound effect
        unpauseSound.Play();

        // Enable player movement, shooting, and UI
        playerCamera.GetComponent<FirstPersonController>().enabled = true;
        playerController.enabled = true;
        shotManager.SetActive(true);
        otherUI.SetActive(true);

        // Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Disable pause menu
        pauseMenu.SetActive(false);
    }

    private void OnGUI()
    {
        if (isPaused)
        {
            /* 
            1. Creates a text box / rect(angle) object at the set X and Y coordinates
            2. Creates and instance of GUIStyle which defines how GUI.Label will look
            3. Sets the desired font, font size, and color
            4. Center aligns the text
            5. Displays the message as defined by the previous parameters
            */

            //1.
            Rect textRect = new Rect(0, 0, Screen.width, Screen.height);

            //2.
            GUIStyle style = new GUIStyle();

            //3.
            style.font = font;
            style.fontSize = fontSize;
            style.normal.textColor = new Color32(252, 3, 3, 255);

            //4.
            style.alignment = TextAnchor.MiddleCenter;

            //5.
            GUI.Label(textRect, "PAUSE", style);
        }
    }
}