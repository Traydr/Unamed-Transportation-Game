using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() // Tied to new game button, loads the next scene from the build index, in the case the main game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() // Tied to the quit button, this exists the application
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
