using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using UnityEngine;

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
                Debug.Log("Event EVLU"); // When adding the additional input data, make sure to add to these debug logs so that they represent whats happening
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

    }

    void EventShopStockRefresh() // Should activate every 7D or 168H for each shop and add to the stock of each of the current items
    {

    }

    void EventLocationProductChange() // Should switch a product in a shop with another one and update the relevant columns in the table
    {

    }
}
