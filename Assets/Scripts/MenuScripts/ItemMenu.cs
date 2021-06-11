using System;
using TMPro;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    public GameObject sellMenu; public GameObject buyMenu; public GameObject gameHandler; public GameObject player;
    public GameObject inventoryMenu;
    public int numRowsInMenu = 4; public int numColInMenu = 4;

    void Start()
    {
        Debug.Log("ItemMenu.Start");
    }

    // Is called from the Open button, takes the last target public transform and then opens the correct menu depending
    // the objects tag, then gets the index of the location and calls the WriteFromDataBaseToMenu() function
    public void UIDetection()
    {
        Transform currentLocation = player.GetComponent<PlayerMovement>().lastTarget;
        string locIndex = FindLocationIdOfCurrentLocation();

        if (currentLocation.CompareTag("Shops")) // Takes the tag to open correct menu, then gets the data from DB and then writes it to the menu
        {
            buyMenu.SetActive(true);
            WriteFromDataBaseToMenu(locIndex, buyMenu, false);
        }
        else if (currentLocation.CompareTag("Cities"))
        {
            sellMenu.SetActive(true);
            WriteFromDataBaseToMenu(locIndex, sellMenu, true);
        }
    }

    private string FindLocationIdOfCurrentLocation() // Finds the ID of the location that the player is currently in
    {
        Transform currentLocation = player.GetComponent<PlayerMovement>().lastTarget;
        string[,] locIndexArray = DataBaseConnector.DataBaseLocationSelect();
        string locIndex = "-1";

        for (int i = 0; i < 6; i++)
        {
            if (currentLocation.transform.name == locIndexArray[i, 1])
            {
                locIndex = locIndexArray[i, 0];
            }
        }

        return locIndex;
    }

    private string FindProductIdOfInputString(string productName) // Finds the product ID from the name of a product
    {
        string prodIndex = "-1"; int counter = 0; bool productNameFound = false;
        string[,] productArray;

        while (productNameFound == false)
        {
            productArray = DataBaseConnector.DataBaseProductsSelect("ProductID", counter.ToString());

            if (productArray[0, 1] == productName)
            {
                productNameFound = true;
                prodIndex = productArray[0, 0];
            }
            else
            {
                counter += 1;
            }
        }

        return prodIndex;
    }

    public void InitiateInventoryMenu() // Updates items on the inventory menu from the database
    {
        string[,] rowElements = new string[7, 4];
        int[] rowChildIndex = FindRowsWithinMenu(inventoryMenu);

        // Goes through all the items in the PlayerInvetory table and assignes them to the 2d array of strings
        for (int i = 0; i < 7; i++)
        {
            string[] tempElements = gameHandler.GetComponent<DataBaseConnector>().DataBasePlayerInventorySelectForInventoryMenu(i.ToString());
            rowElements[i, 0] = tempElements[0];
            rowElements[i, 1] = tempElements[1];
            rowElements[i, 2] = tempElements[2];
            rowElements[i, 3] = tempElements[3];
        }

        // Goes through all the rows in the menu and assigns them values from the 2d array
        for (int x = 0; x < rowChildIndex.Length; x++)
        {
            GameObject tempRow = inventoryMenu.transform.GetChild(rowChildIndex[x]).gameObject;
            WriteRow(tempRow, rowElements[x, 0], float.Parse(rowElements[x, 2]),int.Parse(rowElements[x, 3]));
        }
        
        // Find row indexes and then paste whats in rowElements onto those rows
    }

    public void InventoryMenuToggleActive() // toggles the inventory menu being active
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
    }

    // Takes a location index and then matches it to one inthe productlocation table and updates the menu
    // so that it displays what a particular location has
    void WriteFromDataBaseToMenu(string locIndex, GameObject menu, bool isSelling) 
    {
        string[,] arrMenuData = DataBaseConnector.DataBaseProductLocationSelect("LocationID", locIndex);
        int[] rowChildindex = FindRowsWithinMenu(menu);
        
        // Goes through each item in a city or shop and writes them to each row
        for (int i = 0; i < 4; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildindex[i]).gameObject;
            string[,] getProduct = DataBaseConnector.DataBaseProductsSelect("ProductID", arrMenuData[i, 0]);
            string[] playerInv = gameHandler.GetComponent<DataBaseConnector>().DataBasePlayerInventorySelect("ProductID", arrMenuData[i, 0]);
            string itemString = getProduct[0, 1];
            float priceFloat = float.Parse(arrMenuData[i, 3]);
            int stockInt = 0;

            // If the location is a city then take the stock from the player inventory
            // Otherwise take the stock from the productLocation table
            stockInt = int.Parse(isSelling ? playerInv[2] : arrMenuData[i, 2]);

            WriteRow(tempRow, itemString, priceFloat, stockInt);
        }
    }

    // Gets called when the sell button in the sell menu is pressed
    // Takes the items from the menu and then resolve what was sold and what actions should be taken
    public void ResolveSell(GameObject menu)
    {
        string[] rowElements;
        string locationId = FindLocationIdOfCurrentLocation();

        int[] rowChildIndex = FindRowsWithinMenu(menu);

        // Goes through each row in the menu
        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            rowElements = ReadRow(tempRow);
            string itemName = rowElements[0];
            float itemPrice = float.Parse(rowElements[1]);
            int itemStockInInventory = int.Parse(rowElements[2]);
            int itemToSell = 0;
            string currentProductId = FindProductIdOfInputString(itemName);

            // If the input field was empty then itemToSell is assigned 0
            // if it wasn't empty then it takes whatever value was in it
            itemToSell = rowElements[3] == "" ? 0 : int.Parse(rowElements[3]);

            // Checks to make sure that certain conditions are met before activating the sell
            if (itemToSell < 0) // if the number of items to sell is negative then an error message is displayed
            {
                gameHandler.GetComponent<ErrorGui>().enabled = true;
                gameHandler.GetComponent<ErrorGui>().errorMessage = "To Sell is negative";
            }
            else if (itemToSell > itemStockInInventory) // If there are more items to sell than in inventory an error message is displayed
            {
                gameHandler.GetComponent<ErrorGui>().enabled = true;
                gameHandler.GetComponent<ErrorGui>().errorMessage = "Not enough stock in inventory";
            }
            else if (itemToSell == 0) // If the items to sell was 0 then it is skipped
            {

            }
            else // If it passes all the conditions then the correct changes are made
            {
                // The revenue is calculated, so is the new amount of stock that is to be inputed
                int revenue = Convert.ToInt32(Math.Round(itemToSell * itemPrice));
                itemStockInInventory -= itemToSell;
                
                // The data displayed on the rows are updated and the input field are wiped
                WriteRow(tempRow, itemName, itemPrice, itemStockInInventory);
                WipeInput(tempRow);
                
                // The money increases by the revenue value
                gameHandler.GetComponent<GameMoneyHandler>().MoneyChange(revenue, true);
                
                // The stock changes in PlayerInventory and the change is recorded in the Changes table
                gameHandler.GetComponent<DataBaseConnector>().DataBasePlayerInventoryInput(int.Parse(locationId), itemPrice, itemToSell, true, true);
                gameHandler.GetComponent<DataBaseConnector>().DataBaseProductChangesInsert(int.Parse(currentProductId), int.Parse(locationId), itemPrice, itemToSell);
            }

            // The Input field in the row is wiped weather or not the sale succeeds
            WipeInput(tempRow);
        }
    }

    // Gets called when the buy button in the buy menu is pressed
    // Takes the items from the menu and then resolve what was bought and what actions should be taken
    public void ResolveBuy(GameObject menu) 
    {
        string locationId = FindLocationIdOfCurrentLocation();
        int[] rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            string[] rowElements = ReadRow(tempRow);
            
            string itemName = rowElements[0]; float itemPrice = float.Parse(rowElements[1]);
            int itemStockInShop = int.Parse(rowElements[2]); int itemToBuy = 0;
            string currentProductId = FindProductIdOfInputString(itemName);

            // If the input field was empty then itemToSell is assigned 0
            // if it wasn't empty then it takes whatever value was in it
            if (rowElements[3] == "")
            {
                itemToBuy = 0;
            }
            else
            {
                itemToBuy = int.Parse(rowElements[3]);
            }

            // Checks to make sure that certain conditions are met before activating the buying of stock
            if (itemToBuy < 0) // if the number of items to buy is negative then an error message is displayed
            {
                gameHandler.GetComponent<ErrorGui>().enabled = true;
                gameHandler.GetComponent<ErrorGui>().errorMessage = "Cannot buy a negative amount";
            }
            else if (itemToBuy > itemStockInShop) // If there are more items to buy than in inventory an error message is displayed
            {
                gameHandler.GetComponent<ErrorGui>().enabled = true;
                gameHandler.GetComponent<ErrorGui>().errorMessage = "Not enough stock in shop to buy";
            }
            else if (itemToBuy == 0) // If the items to sell was 0 then it is skipped
            {

            }
            else // If it passes all the conditions then the correct changes are made
            {
                // The total cost is calculated, so is the new amount of stock that is to be inputed
                int cost = Convert.ToInt32(Math.Round(itemToBuy * itemPrice));
                itemStockInShop -= itemToBuy;
                
                // The data displayed on the rows are updated and the input field are wiped
                WriteRow(tempRow, itemName, itemPrice, itemStockInShop);
                WipeInput(tempRow);
                
                // The money decrease by the cost value
                gameHandler.GetComponent<GameMoneyHandler>().MoneyChange(cost, false);
                
                // The correct amount of items are added to the player inventory
                // The correct amount of items are removed from the locations stock in productLocations table
                // The change is recorded in the ProductChanges Table
                gameHandler.GetComponent<DataBaseConnector>().DataBasePlayerInventoryInput(int.Parse(FindProductIdOfInputString(itemName)), itemPrice, itemToBuy, true, false);
                DataBaseConnector.DataBaseProductLocationUpdate("Stock", itemStockInShop.ToString(), "ProductID", FindProductIdOfInputString(itemName), "LocationID", locationId);
                gameHandler.GetComponent<DataBaseConnector>().DataBaseProductChangesInsert(int.Parse(currentProductId), int.Parse(locationId), itemPrice, itemToBuy);
            }

            WipeInput(tempRow);
        }
    }

    // Goes through the children of a menu game objects and checks if the tag of the child object is row
    // If it is then the index of the child is assigned to the array of integers, that array is then returned at the end
    int[] FindRowsWithinMenu(GameObject menu) // Finds the child index of any game objects with the tag 'Row'
    {
        int menuChildCount = menu.transform.childCount;
        int localNumRowsInMenu = numRowsInMenu;
        int currentIndex = 0;

        if (menu == inventoryMenu) // An exception for inventory menu which has 7 rows insteand of 4
        {
            localNumRowsInMenu = 7;
        }
        
        int[] rowChildIndex = new int[localNumRowsInMenu];

        for (int i = 0; i < menuChildCount; i++)
        {
            GameObject tempChild = menu.transform.GetChild(i).gameObject;

            if (tempChild.transform.CompareTag("Row"))
            {
                rowChildIndex[currentIndex] = i;
                currentIndex += 1;
            }
        }

        return rowChildIndex;
    }

    string[] ReadRow(GameObject row) // Reads the item, price, stock and input values from a certain row and then outputs them as an array of strings
    {
        string[] rowElements = new string[numColInMenu];
        
        rowElements[0] = row.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[1] = row.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[2] = row.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[3] = row.transform.GetChild(3).gameObject.GetComponent<TMP_InputField>().text;

        return rowElements;
    }

    void WriteRow(GameObject row, string itemString, float priceFloat, int stockInt) // Writes to a certain row gameobject 
    {
        row.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = itemString;
        row.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = priceFloat.ToString();
        row.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = stockInt.ToString();
    }

    void WipeInput(GameObject row) // Sets the input text field to contain nothing
    {
        row.transform.GetChild(3).gameObject.GetComponent<TMP_InputField>().text = "";
    }

    public void WipeAllInputs(GameObject menu) // Wipes all inputs on all rows in a certain menu
    {
        int[] rowChildIndex = FindRowsWithinMenu(menu);

        foreach (int tempIndex in rowChildIndex)
        {
            GameObject tempRow = menu.transform.GetChild(tempIndex).gameObject;
            WipeInput(tempRow);
        }
    }
    
}
