using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject gameHandler;

    // Public test vars below
    
    void Start()
    {
        Debug.Log("GameHandler.Start");
    }

    // EVERYTHING BELOW IS FOR TESTING
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            TestAllDB();
        }
    }

    void TestAllDB()
    {
        // Table: Location
        Debug.Log("Table: Location");
        string[,] locSelect = new string[6, 4]; locSelect = gameHandler.GetComponent<DBconnector>().DBLTSelect();

        for (int i = 0; i < 6; i++)
        {
            string logFormat;
            logFormat = string.Format("{0} LocID; {1} LocEdName; {2} LodDisName; {3} CityT_or_ShopF;", locSelect[i, 0], locSelect[i, 1], locSelect[i, 2], locSelect[i, 3]);
            Debug.Log(logFormat);
        }

        // Table: Product
        Debug.Log("Table: Product");
        string[,] prodSelect = new string[1, 6];

        for (int i = 0; i < 7; i++)
        {
            prodSelect = gameHandler.GetComponent<DBconnector>().DBPDSelect("ProductID", Convert.ToString(i));
            string logFormat;
            logFormat = string.Format("{0} ProdID; {1} ProdName; {2} BPrice; {3} ProdVol; {4} ProdWeight; {5} PED", prodSelect[0, 0], prodSelect[0, 1], prodSelect[0, 2], prodSelect[0, 3], prodSelect[0, 4], prodSelect[0, 5]);
            Debug.Log(logFormat);
        }

        // Table: ProductLocation
        Debug.Log("Table: ProductLocation");
        string[,] plTable = new string[4, 5];
        plTable = gameHandler.GetComponent<DBconnector>().DBPDLTSelect("LocationID","2");
        
        for (int i = 0; i < 4; i++)
        {
            string logFormat;
            logFormat = string.Format("{0} PID; {1} LID; {2} Stock; {3} LPrice; {4} Tag;", plTable[i, 0], plTable[i, 1], plTable[i, 2], plTable[i, 3], plTable[i, 4]);
            Debug.Log(logFormat);

        }

        gameHandler.GetComponent<DBconnector>().DBPDLTUpdate("Stock", "50", "ProductID", "1", "LocationID", "2");

        // Table: ProductChanges
        Debug.Log("Table: ProductChanges");
        string[,] pdchSelect = new string[50, 6]; 
        pdchSelect = gameHandler.GetComponent<DBconnector>().DBPDCHSelect("ChangeID","0");

        int maxIndecChanges = gameHandler.GetComponent<DBconnector>().DBPDCHGetMaxChangeID();
        string logFormat1;
        logFormat1 = string.Format("{0} CHID; {1} PDID; {2} LCID; {3} NP; {4} NS; {5} IGT", pdchSelect[maxIndecChanges - 1, 0], pdchSelect[maxIndecChanges, 1], pdchSelect[maxIndecChanges, 2], pdchSelect[maxIndecChanges, 3], pdchSelect[maxIndecChanges, 4], pdchSelect[maxIndecChanges, 5]);
        Debug.Log(logFormat1);

        gameHandler.GetComponent<DBconnector>().DBPDCHInsert(0, 0, 0.1f, 420);

        // Probably fine if above others are fine | really dont want to go through them
        // Table: PlayerInventory
        Debug.Log("Table: PlayerInventory");



        // Table: Upgrades
        Debug.Log("Table: Upgrades");

    }
}
