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

    public void SaveGame()
    {
        Debug.Log("SaveGame Called");
        int tempMoney = gameHandler.GetComponent<MoneyDisplayChange>().ReadMoney();
        int tempTotalTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        string[] saveData = {tempMoney.ToString(), tempTotalTime.ToString() };

        WriteFile(saveData, CreateFilePath(currentGameId));
    }

    public void LoadGame()
    {
        string[] saveData = ReadFile(CreateFilePath(currentGameId));
        
        gameHandler.GetComponent<MoneyDisplayChange>().WriteMoney(int.Parse(saveData[0]));
        gameHandler.GetComponent<InGameTime>().SetTimeFromTotalHours(int.Parse(saveData[1]));
    }

    string CreateFilePath(int gameId)
    {
        string finalPath = saveGameFilePath + gameId + saveGameFileExtention;
        return finalPath;
    }

    void CreateFile(string filePath)
    {
        File.CreateText(filePath).Close();
    }

    void CreateFolder(string filePath)
    {
        Directory.CreateDirectory(filePath);
    }

    bool CheckFile(string filePath)
    {
        bool fileExists;
        fileExists = System.IO.File.Exists(filePath);
        return fileExists;
    }

    bool CheckFolder(string filePath)
    {
        bool folderExists;
        folderExists = System.IO.Directory.Exists(filePath);
        return folderExists;
    }

    void WriteFile(string[] saveData, string filePath) // A Reminder that WriteAllText Sucks
    {
        string saveDataString = saveData[0] + Environment.NewLine + saveData[1];
        File.WriteAllText(filePath, saveDataString);
    }

    string[] ReadFile(string filePath)
    {
        string[] fileOutput = new string[2];

        fileOutput = System.IO.File.ReadAllLines(filePath);

        return fileOutput;
    }
}
