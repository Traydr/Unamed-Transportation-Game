using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class FileHandler : MonoBehaviour
{
    public GameObject gameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FileHandler.Start");
    }

    public void SaveGame(int saveGameId)
    {
        Debug.Log("SaveGame Called");
        int tempMoney = gameHandler.GetComponent<MoneyDisplayChange>().ReadMoney();
        int tempTotalTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        string[] saveData = {tempMoney.ToString(), tempTotalTime.ToString() };
        string saveFileName = $"SaveGame{saveGameId}";

        File.WriteAllLines(saveFileName, saveData);
    }

    public void LoadGame(int loadGameId)
    {
        
    }
}
