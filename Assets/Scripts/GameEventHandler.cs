using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
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
            case "EVLU":
                Debug.Log("Event EVLU"); // When adding the additional input data, make sure to add to these debug logs so that they show whats happening
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
        bool locFound = false; bool prodFound = false;
        int currentIndexForDataArray = 0;
        string[,] selectResultFromChanges;
        string[] selectResultsFromProductLocation;
        string[,] allRelevantDataForLocations = new string[24,5]; // ProductId, LocationId, ChangeInStock, CurrentPrice, CurrentStock
        
        currentTime = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        timeSinceLastCheck = currentTime - 24;
        selectResultFromChanges =
            gameHandler.GetComponent<DBconnector>().DataBaseProductChangesSelectWithinLast24Hours(timeSinceLastCheck.ToString());

        for (int i = 0; i < selectResultFromChanges.Length / 6; i++)
        {
            selectResultsFromProductLocation =
                gameHandler.GetComponent<DBconnector>().DataBaseProductLocationSelectSpecificProduct(selectResultFromChanges[i, 1], selectResultFromChanges[i, 2]);

            //allRelevantDataForLocations needs to be inputted with the data and also add up any changes
            // to calculate the new price I need: PED, Current Price, Last Stock, Current Stock
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
}
