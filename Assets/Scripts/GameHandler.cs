using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject gameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.Start");

        // Testing of public functions
        string[,] arrayOfTable = new string[4, 5];
        arrayOfTable = gameHandler.GetComponent<DBconnector>().DBPDLTSelect("LocationID","2");
        
        for (int i = 0; i < 4; i++)
        {
            string logFormat;
            logFormat = string.Format("{0} PID; {1} LID; {2} Stock; {3} LPrice; {4} Tag;", arrayOfTable[i, 0], arrayOfTable[i, 1], arrayOfTable[i, 2], arrayOfTable[i, 3], arrayOfTable[i, 4]);
            Debug.Log(logFormat);

        }
        // End of Testing
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
