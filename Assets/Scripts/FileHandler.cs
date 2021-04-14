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

    public void SaveGame() // Saves the game by copying the amount of money and the current time in hours and then sends it to WriteFile()
    {
        Debug.Log("SaveGame Called");
        int tempMoney = gameHandler.GetComponent<MoneyDisplayChange>().ReadMoney();
        int tempTotalTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        string[] saveData = {tempMoney.ToString(), tempTotalTime.ToString() };

        WriteFile(saveData, CreateFilePath(currentGameId));
    }

    public void LoadGame() // gets saveData from the file and then writes it to the money being displayed and sets the time
    {
        string[] saveData = ReadFile(CreateFilePath(currentGameId));
        
        gameHandler.GetComponent<MoneyDisplayChange>().WriteMoney(int.Parse(saveData[0]));
        gameHandler.GetComponent<InGameTime>().SetTimeFromTotalHours(int.Parse(saveData[1]));
    }

    string CreateFilePath(int gameId) // Creates a filepath from diffrent strings and returns it as a single string
    {
        string finalPath = saveGameFilePath + gameId + saveGameFileExtention;
        return finalPath;
    }

    void CreateFile(string filePath) // Creates a specific file
    {
        File.CreateText(filePath).Close();
    }

    void CreateFolder(string filePath) // Creates the folders leading to a file
    {
        Directory.CreateDirectory(filePath);
    }

    bool CheckFile(string filePath) // Checks if the file exists
    {
        bool fileExists;
        fileExists = System.IO.File.Exists(filePath);
        return fileExists;
    }

    bool CheckFolder(string filePath) // Checks if the folder exists
    {
        bool folderExists;
        folderExists = System.IO.Directory.Exists(filePath);
        return folderExists;
    }

    void WriteFile(string[] saveData, string filePath) // Writes the inputed data to file
    {
        string saveDataString = saveData[0] + Environment.NewLine + saveData[1];
        File.WriteAllText(filePath, saveDataString);
    }

    string[] ReadFile(string filePath) // Reads the data from file and returns it as an array of strings
    {
        string[] fileOutput = System.IO.File.ReadAllLines(filePath);

        return fileOutput;
    }
}
