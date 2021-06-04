using System;
using UnityEngine;
using System.IO;

public class FileHandler : MonoBehaviour
{
    public GameObject gameHandler;
    public int currentGameId = 0;
    private string _saveGameDirectoryPath = @"Assets/SaveGames/";
    private string _saveGameFileName = "save.txt";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FileHandler.Start");
    }

    public void FileAction(char action)
    {
        switch (action)
        {
            case 's':
                SaveGame();
                break;
            case 'l':
                LoadGame();
                break;
            default:
                Console.WriteLine("Error Incorrect FileAction");
                break;
        }
    }
    
    // Saves the game by copying the amount of money and the current time in hours into an array and then calls the WriteFile() function
    // TODO: This function should save data to the file of the current gameId
    private void SaveGame() 
    {
        UpdateSaveGameFileName();
        VerifyFileContent(CreateFilePath(currentGameId));
        int tempMoney = gameHandler.GetComponent<MoneyDisplayChange>().ReadMoney();
        int tempTotalTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        string[] saveData = {tempMoney.ToString(), tempTotalTime.ToString() };

        WriteFile(saveData, CreateFilePath(currentGameId));
    }
    
    // gets saveData from the file and then writes it to the money being displayed and sets the time
    // TODO: This function should modify the gameId pointer, then load any data that does not need the pointer
    private void LoadGame() 
    {
        UpdateSaveGameFileName();
        VerifyFileContent(CreateFilePath(currentGameId));
        string[] saveData = ReadFile(CreateFilePath(currentGameId));
        
        gameHandler.GetComponent<MoneyDisplayChange>().WriteMoney(int.Parse(saveData[0]));
        gameHandler.GetComponent<InGameTime>().SetTimeFromTotalHours(int.Parse(saveData[1]));
    }

    // Verifies that the file exists, if not then it creates it
    private void VerifyFileLocation(string filePath)
    {
        if (File.Exists(filePath) != true)
        {
            Directory.CreateDirectory(filePath);
            File.Create(filePath);
        } 
    }

    // Verifies that the file has content
    // TODO: Verify that the file has data in correct format
    private void VerifyFileContent(string filePath)
    {
        VerifyFileLocation(filePath);
        if (!(ReadFile(filePath) is null)) return;
        string[] defaultGameData = {"2000", "0" };
        WriteFile(defaultGameData, filePath);
    }

    private void UpdateSaveGameFileName()
    {
        _saveGameFileName = $"save{currentGameId}.txt";
    }
    
    // Creates a filepath from different strings and returns it as a single string
    private string CreateFilePath(int gameId) 
    {
        return _saveGameDirectoryPath + gameId + _saveGameFileName;
    }

    // Writes the inputted data to file
    // TODO: Rewrite this function so that more data will be written (when implemented)
    private void WriteFile(string[] saveData, string filePath) 
    {
        string saveDataString = saveData[0] + Environment.NewLine + saveData[1];
        File.WriteAllText(filePath, saveDataString);
    }

    // Reads the data from file and returns it as an array of strings
    private string[] ReadFile(string filePath) 
    {
        string[] fileOutput = File.ReadAllLines(filePath);
        return fileOutput;
    }
}
