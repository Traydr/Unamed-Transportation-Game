using System;
using UnityEngine;
using System.IO;

public class FileHandler : MonoBehaviour
{
    public GameObject gameHandler;
    public string saveGameFilePath = @"Assets/SaveGames/save";
    public string saveGameFileExtention = ".txt";
    public int currentGameId = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FileHandler.Start");
    }
    
    // Saves the game by copying the amount of money and the current time in hours into an array and then calls the WriteFile() function
    public void SaveGame() 
    {
        Debug.Log("SaveGame Called");
        int tempMoney = gameHandler.GetComponent<MoneyDisplayChange>().ReadMoney();
        int tempTotalTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        string[] saveData = {tempMoney.ToString(), tempTotalTime.ToString() };

        WriteFile(saveData, CreateFilePath(currentGameId));
    }
    
    // gets saveData from the file and then writes it to the money being displayed and sets the time
    public void LoadGame() 
    {
        string[] saveData = ReadFile(CreateFilePath(currentGameId));
        
        gameHandler.GetComponent<MoneyDisplayChange>().WriteMoney(int.Parse(saveData[0]));
        gameHandler.GetComponent<InGameTime>().SetTimeFromTotalHours(int.Parse(saveData[1]));
    }
    
    // Creates a filepath from diffrent strings and returns it as a single string
    string CreateFilePath(int gameId) 
    {
        string finalPath = saveGameFilePath + gameId + saveGameFileExtention;
        return finalPath;
    }

    // Creates a specific file
    void CreateFile(string filePath)
    {
        File.CreateText(filePath).Close();
    }
 
    // Creates the folders leading to a file
    void CreateFolder(string filePath)
    {
        Directory.CreateDirectory(filePath);
    }

    // Checks if the file exists
    bool CheckFile(string filePath) 
    {
        bool fileExists;
        fileExists = System.IO.File.Exists(filePath);
        return fileExists;
    }

    // Checks if the folder exists
    bool CheckFolder(string filePath) 
    {
        bool folderExists;
        folderExists = System.IO.Directory.Exists(filePath);
        return folderExists;
    }

    // Writes the inputed data to file
    void WriteFile(string[] saveData, string filePath) 
    {
        string saveDataString = saveData[0] + Environment.NewLine + saveData[1];
        File.WriteAllText(filePath, saveDataString);
    }

    // Reads the data from file and returns it as an array of strings
    string[] ReadFile(string filePath) 
    {
        string[] fileOutput = System.IO.File.ReadAllLines(filePath);

        return fileOutput;
    }
}
