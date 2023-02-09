using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Select : MonoBehaviour
{
    // Selects scene 
    public void OpenScene(string sceneName)
    {
        // Loads scene based on text input and disables cursor visibility
        SceneManager.LoadScene(sceneName);
        Cursor.visible = false;
    }
}
