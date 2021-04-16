﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEventHandler : MonoBehaviour
{
    public GameObject gameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameEventHandler.Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEventRequest(string requestType)
    {
        switch (requestType)
        {
            case "ELPU":
                Debug.Log("Event ELPU"); // When adding the additional input data, make sure to add to these debug logs so that they show whats happening
                EventLocationPriceUpdate();
                break;
            case "ESSR":
                Debug.Log("Event ESSR");
                EventShopStockRefresh();
                break;
            case "ELPC":
                Debug.Log("Event ELPC");
                EventLocationProductChange();
                break;
            case "ECC":
                Debug.Log("Event ECC");
                EventCityConsumption();
                break;
            default:
                Debug.Log("ERROR, INVALID EVENT REQUEST");
                gameHandler.GetComponent<GUIWindowCreation>().enabled = true;
                gameHandler.GetComponent<GUIWindowCreation>().errorMessage = "INVALID EVENT REQUEST";
                break;
        }
    }

    void EventLocationPriceUpdate() // Should check for any changes in ProductChanges in the last 24 hrs and then update the prices of diffrent products at a location
    {
        int currentTime = 0; int timeSinceLastCheck = 0;
        int indexCounter = 0;
        bool locFound = false; bool prodFound = false;
        int currentIndexForDataArray = 0;
        string[,] allRelevantDataForLocations = new string[24,6]; // ProductId, LocationId, CurrentPrice, CurrentStock, ChangeInStock, PED
        
        currentTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        timeSinceLastCheck = currentTime - 24;
        string[,] selectResultFromChanges = gameHandler.GetComponent<DBconnector>().DataBaseProductChangesSelectWithinLast24Hours(timeSinceLastCheck.ToString());
        
        // Goes through ProductLocation database and copies all relevant data to a 2d Array
        // These items include the productId, LocaitonId, CurrentPrice, CurrentStock and PED here
        for (int x = 0; x < 6; x++) 
        {
            string[,] resultsFromProducLocationForLocation =
                DBconnector.DataBaseProductLocationSelect("LocationID", x.ToString());

            for (int z = 0; z < 4; z++)
            {
                string[,] tempProduct = DBconnector.DataBaseProductsSelect("ProductID", resultsFromProducLocationForLocation[z, 0]);
                allRelevantDataForLocations[indexCounter, 0] = resultsFromProducLocationForLocation[z, 0];
                allRelevantDataForLocations[indexCounter, 1] = resultsFromProducLocationForLocation[z, 1];
                allRelevantDataForLocations[indexCounter, 2] = resultsFromProducLocationForLocation[z, 3];
                allRelevantDataForLocations[indexCounter, 3] = resultsFromProducLocationForLocation[z, 2];
                allRelevantDataForLocations[indexCounter, 4] = 0.ToString();
                allRelevantDataForLocations[indexCounter, 5] = tempProduct[0, 5];
                indexCounter += 1;
            }
        }
        
        // Finds the correct index where the changes will be inputted to
        for (int i = 0; i < selectResultFromChanges.Length / 3; i++)
        {
            if (selectResultFromChanges[i, 0] is null)
            {
                
            }
            else
            {
                int locationStartIndex = int.Parse(selectResultFromChanges[i, 1]) * 4;
                int productLocationIndex = locationStartIndex;
                bool productLocationIndexFound = false;

                for (int c = locationStartIndex; c < locationStartIndex + 4; c++)
                {
                    if (allRelevantDataForLocations[locationStartIndex, 0] == selectResultFromChanges[i, 0])
                    {
                        productLocationIndexFound = true;
                    }
                    else if (productLocationIndexFound != true)
                    {
                        productLocationIndex += 1;
                    }
                }

                if (i == 0)
                {
                    
                }
                else
                {
                    Debug.Log(selectResultFromChanges[i, 2]);
                    Debug.Log(productLocationIndex);
                    Debug.Log(i);
                    Debug.Log(allRelevantDataForLocations[productLocationIndex, 4]);
                    
                    
                    allRelevantDataForLocations[productLocationIndex, 4] = Convert.ToString(int.Parse(allRelevantDataForLocations[productLocationIndex, 4]) + int.Parse(selectResultFromChanges[i, 2]));
                }
                
                
            }
        }

        for (int v = 0; v < allRelevantDataForLocations.Length / 6; v++)
        {
            if (allRelevantDataForLocations[v, 4] == "0")
            {
                
            }
            else
            {
                float tempCurrentPrice = 0f; 
                
                string tempProductId = allRelevantDataForLocations[v, 0];
                string tempLocationId = allRelevantDataForLocations[v, 1];
                float tempPed = float.Parse(allRelevantDataForLocations[v, 5]);
                bool hasCurrentPriceFailed = float.TryParse(allRelevantDataForLocations[v, 2], out tempCurrentPrice);
                // TESTING
                Debug.Log(hasCurrentPriceFailed);
                Debug.Log(allRelevantDataForLocations[v, 3]); Debug.Log(allRelevantDataForLocations[v, 4]);
                
                //float tempCurrentPrice = float.TryParse(allRelevantDataForLocations[v, 2], hasCurrentPriceFailed);
                int tempLaststock = int.Parse(allRelevantDataForLocations[v, 3]) + int.Parse(allRelevantDataForLocations[v, 4]);
                int tempCurrentStock = int.Parse(allRelevantDataForLocations[v, 3]);
                
                float newPrice = gameHandler.GetComponent<Economics>().CalcChangeInPrice(tempPed, tempCurrentPrice, tempLaststock, tempCurrentStock);
                
                // Product Location is not updating
                DBconnector.DataBaseProductLocationUpdate("LocalPrice", newPrice.ToString(), "ProductID", tempProductId, "LocationID", tempLocationId);
                gameHandler.GetComponent<DBconnector>().DataBaseProductChangesInsert(int.Parse(tempProductId), int.Parse(tempLocationId), newPrice, tempCurrentStock);
            }
        }
    }

    void EventShopStockRefresh() // Should activate every 7D or 168H for each shop and add to the stock of each of the current items
    {
        float successRate = 0.3f; // 0 to 1, a number higher than this will mean that the event succeeds
        int numLocationsAffected = 0; // Wil randomise the number of locations that get affected by the event 1 to 6
        int numItemsWithinLocationAffected = 0; // Will hold a random number from 1 to 4 that indicates how many items get affected

        if (Random.Range(0f, 1f) > successRate)
        {
            numLocationsAffected = Random.Range(0, 6);
            numItemsWithinLocationAffected = Random.Range(0, 4);
        }
    }

    void EventLocationProductChange() // Should switch a product in a shop with another one and update the relevant columns in the table
    {
        float successRate = 0.9f; // 0 to 1, a number higher than this will mean that the event succeeds
        int numLocationsAffected = 0; // Wil randomise the number of locations that get affected by the event 1 to 6
        int numItemsWithinLocationAffected = 0; // Will hold a random number from 1 to 4 that indicates how many items get affected

        if (Random.Range(0f, 1f) > successRate)
        {
            numLocationsAffected = Random.Range(0, 6);
            numItemsWithinLocationAffected = Random.Range(0, 4);
        }
    }

    void EventCityConsumption()
    {
        float successRate = 0.1f; // 0 to 1, a number higher than this will mean that the event succeeds
        int numLocationsAffected = 0; // Wil randomise the number of locations that get affected by the event 1 to 6
        int numItemsWithinLocationAffected = 0; // Will hold a random number from 1 to 4 that indicates how many items get affected

        if (Random.Range(0f, 1f) > successRate)
        {
            numLocationsAffected = Random.Range(0, 6);
            numItemsWithinLocationAffected = Random.Range(0, 4);
        }
    }
}
