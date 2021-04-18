using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // GameHandler
    public GameObject gameHandler;
    
    void Start()
    {
        Debug.Log("KeyboardControls.Start");
    }

    void FixedUpdate()
    {
        if (Input.GetKey("j")) // Adds 100 money if j key is pressed
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, true);
        }
        
        if (Input.GetKey("h")) // Subtracts 100 money if h key is pressed
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, false);
        }
        
        if (Input.GetKey("k")) // Increases in game time by 1 hour if k key is pressed
        {
            gameHandler.GetComponent<InGameTime>().UpdateTime(1);
        }

        if (Input.GetKey("m")) // Saves the game if m key is pressed
        {
            gameHandler.GetComponent<FileHandler>().SaveGame();
        }

        if (Input.GetKey("n")) // Loads a save game if n key is pressed 
        {
            gameHandler.GetComponent<FileHandler>().LoadGame();
        }
        
    }
}
