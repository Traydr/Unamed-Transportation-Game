using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // GameHandler
    public GameObject gameHandler;
    private GameMoneyHandler _gameMoneyHandler;
    private GameTimeHandler _gameTimeHandler;
    private FileHandler _fileHandler;

    void Start()
    {
        _fileHandler = gameHandler.GetComponent<FileHandler>();
        _gameTimeHandler = gameHandler.GetComponent<GameTimeHandler>();
        _gameMoneyHandler = gameHandler.GetComponent<GameMoneyHandler>();
        Debug.Log("KeyboardControls.Start");
    }

    void Update()
    {
        if (Input.GetKey("j")) // Adds 100 money if j key is pressed
        {
            _gameMoneyHandler.MoneyChange(100, true);
        }
        
        if (Input.GetKey("h")) // Subtracts 100 money if h key is pressed
        {
            _gameMoneyHandler.MoneyChange(100, false);
        }
        
        if (Input.GetKey("k")) // Increases in game time by 1 hour if k key is pressed
        {
            _gameTimeHandler.UpdateTime(1);
        }

        if (Input.GetKey("m")) // Saves the game if m key is pressed
        {
            _fileHandler.FileAction('s');
        }

        if (Input.GetKey("n")) // Loads a save game if n key is pressed 
        {
            _fileHandler.FileAction('l');
        }
        
    }
}
