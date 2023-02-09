using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Script : MonoBehaviour
{
    // In settings variable
    private bool inSettings = false;

    // Remove and add variable arrays
    public GameObject[] remove;
    public GameObject[] add;

    // Pause menu sound effects
    public AudioSource unpauseSound;

    // Binary clicking of a gameObject which will not be disabled on click
    public void Enter_Exit()
    {
        if (inSettings == false)
        {
            Enter_Settings();
            inSettings = true;
        }
        else
        {
            Exit_Settings();
            inSettings = false;
        }
    }

    // One way clicking of a gameObject that will be disabled on click
    public void Enter_Only()
    {
        Enter_Settings();
    }

    // Remove and add the assigned gameObjects
    public void Enter_Settings()
    {
        // Play menu sound effect
        unpauseSound.Play();

        // Remove and add the assigned gameObjects
        for (int i = 0; i < remove.Length; i++)
        {
            remove[i].SetActive(false);
        }

        for (int i = 0; i < add.Length; i++)
        {
            add[i].SetActive(true);
        }
    }

    // Inverse of Enter_Settings()
    public void Exit_Settings()
    {
        // Play menu sound effect
        unpauseSound.Play();

        // Inversely remove and add the assigned gameObjects
        for (int i = 0; i < remove.Length; i++)
        {
            remove[i].SetActive(true);
        }

        for (int i = 0; i < add.Length; i++)
        {
            add[i].SetActive(false);
        }
    }
}
