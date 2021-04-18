using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    // Tied to the 'main menu' button in the escape menu, this load the first scene in the build menu
    // In this case the first scene or scnene 0 is the start menu scene
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
