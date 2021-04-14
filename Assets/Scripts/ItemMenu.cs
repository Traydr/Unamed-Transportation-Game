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

    public void UIDetection() // Needs to take the last target from public variable in the player movement script
    {
        Transform currentLocation = player.GetComponent<PlayerMovement>().lastTarget;
        string locIndex = FindLocationIDOfCurrentLocation();

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

    string FindLocationIDOfCurrentLocation() // Finds the ID of the location that the player is currently in
    {
        Transform currentlocation = player.GetComponent<PlayerMovement>().lastTarget;
        string[,] locIndexArray = DBconnector.DataBaseLocationSelect();
        string locIndex = "-1";

        for (int i = 0; i < 6; i++)
        {
            if (currentlocation.transform.name == locIndexArray[i, 1])
            {
                locIndex = locIndexArray[i, 0];
            }
        }

        return locIndex;
    }

    string FindProductIDOfInputString(string productName) // Finds the product ID from the name of a product
    {
        string prodIndex = "-1"; int counter = 0; bool productNameFound = false;
        string[,] productArray;

        while (productNameFound == false)
        {
            productArray = DBconnector.DataBaseProductsSelect("ProductID", counter.ToString());

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

        for (int i = 0; i < 7; i++)
        {
            string[] tempElements = gameHandler.GetComponent<DBconnector>().DataBasePlayerInventorySelectForInventoryMenu(i.ToString());
            rowElements[i, 0] = tempElements[0];
            rowElements[i, 1] = tempElements[1];
            rowElements[i, 2] = tempElements[2];
            rowElements[i, 3] = tempElements[3];
        }

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

    void WriteFromDataBaseToMenu(string locIndex, GameObject menu, bool isSelling) // Takes a location and then matches it to the database and updates the menu so that it displays what a particular location has
    {
        string[,] getProduct; string[] playerInv;
        string[,] arrMenuData = DBconnector.DataBaseProductLocationSelect("LocationID", locIndex);
        int[] rowChildindex = FindRowsWithinMenu(menu);
        
        for (int i = 0; i < 4; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildindex[i]).gameObject;
            getProduct = DBconnector.DataBaseProductsSelect("ProductID", arrMenuData[i, 0]);
            playerInv = gameHandler.GetComponent<DBconnector>().DataBasePlayerInventorySelect("ProductID", arrMenuData[i, 0]);
            string itemString = getProduct[0, 1];
            float priceFloat = float.Parse(arrMenuData[i, 3]);
            int stockInt = 0;

            if (isSelling)
            {
                stockInt = int.Parse(playerInv[2]);
            }
            else
            {
                stockInt = int.Parse(arrMenuData[i, 2]);
            }
            
            WriteRow(tempRow, itemString, priceFloat, stockInt);
        }
    }

    public void ResolveSell(GameObject menu) // needs to check against vehicles weight and volume!!
    {
        string[] rowElements;
        string locationID = FindLocationIDOfCurrentLocation();

        int[] rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            rowElements = ReadRow(tempRow);
            string itemName = rowElements[0];
            float itemPrice = float.Parse(rowElements[1]);
            int itemStockInInventory = int.Parse(rowElements[2]);
            int itemToSell = 0;
            string currentProductID = FindProductIDOfInputString(itemName);

            // Checks that itemToSell contains an integer value
            if (rowElements[3] == "")
            {
                itemToSell = 0;
            }
            else
            {
                itemToSell = int.Parse(rowElements[3]);
            }

            // Checks to make sure that certain conditions are met before activating the sell
            if (itemToSell < 0)
            {
                gameHandler.GetComponent<GUIWindowCreation>().enabled = true;
                gameHandler.GetComponent<GUIWindowCreation>().errorMessage = "To Sell is negative";
            }
            else if (itemToSell > itemStockInInventory)
            {
                gameHandler.GetComponent<GUIWindowCreation>().enabled = true;
                gameHandler.GetComponent<GUIWindowCreation>().errorMessage = "Not enough stock in inventory";
            }
            else if (itemToSell == 0)
            {

            }
            else // Add changes to DB
            {
                int revenue = Convert.ToInt32(Math.Round(itemToSell * itemPrice));
                itemStockInInventory -= itemToSell;
                WriteRow(tempRow, itemName, itemPrice, itemStockInInventory);
                WipeInput(tempRow);
                gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(revenue, true);
                gameHandler.GetComponent<DBconnector>().DataBasePlayerInventoryInput(int.Parse(locationID), itemPrice, itemToSell, true, true);
                gameHandler.GetComponent<DBconnector>().DataBaseProductChangesInsert(int.Parse(currentProductID), int.Parse(locationID), itemPrice, itemToSell);
            }

            WipeInput(tempRow);
        }
    }

    public void ResolveBuy(GameObject menu) // Still losing 
    {
        string locationID = FindLocationIDOfCurrentLocation();
        int[] rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            string[] rowElements = ReadRow(tempRow);
            
            string itemName = rowElements[0]; float itemPrice = float.Parse(rowElements[1]);
            int itemStockInShop = int.Parse(rowElements[2]); int itemToBuy = 0;
            string currentProductID = FindProductIDOfInputString(itemName);

            // Checks that itemToSell contains an integer value
            if (rowElements[3] == "")
            {
                itemToBuy = 0;
            }
            else
            {
                itemToBuy = int.Parse(rowElements[3]);
            }

            // Checks to make sure that certain conditions are met before activating the sell
            if (itemToBuy < 0)
            {
                gameHandler.GetComponent<GUIWindowCreation>().enabled = true;
                gameHandler.GetComponent<GUIWindowCreation>().errorMessage = "Cannot buy a negative amount";
            }
            else if (itemToBuy > itemStockInShop)
            {
                gameHandler.GetComponent<GUIWindowCreation>().enabled = true;
                gameHandler.GetComponent<GUIWindowCreation>().errorMessage = "Not enough stock in shop to buy";
            }
            else if (itemToBuy == 0)
            {

            }
            else // Add changes to DB
            {
                int cost = Convert.ToInt32(Math.Round(itemToBuy * itemPrice));
                itemStockInShop -= itemToBuy;
                WriteRow(tempRow, itemName, itemPrice, itemStockInShop);
                WipeInput(tempRow);
                gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(cost, false);
                gameHandler.GetComponent<DBconnector>().DataBasePlayerInventoryInput(int.Parse(locationID), itemPrice, itemToBuy, true, false);
                DBconnector.DataBaseProductLocationUpdate("Stock", itemStockInShop.ToString(), "ProductID", FindProductIDOfInputString(itemName), "LocationID", locationID);
                gameHandler.GetComponent<DBconnector>().DataBaseProductChangesInsert(int.Parse(currentProductID), int.Parse(locationID), itemPrice, itemToBuy);
            }

            WipeInput(tempRow);
        }
    }

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

    string[] ReadRow(GameObject row) // Reads the item, price, stock and input values from the shop or city menu and then outputs them as an array of strings
    {
        string[] rowElements = new string[numColInMenu]; // Should collapse all these so they take up less space
        
        rowElements[0] = row.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[1] = row.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[2] = row.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text;
        rowElements[3] = row.transform.GetChild(3).gameObject.GetComponent<TMP_InputField>().text;

        return rowElements;
    }

    void WriteRow(GameObject row, string itemString, float priceFloat, int stockInt) // Writes to the shop or city menus
    {
        row.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = itemString;
        row.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = priceFloat.ToString();
        row.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = stockInt.ToString();
    }

    void WipeInput(GameObject row) // Sets the input text field to contain nothing
    {
        row.transform.GetChild(3).gameObject.GetComponent<TMP_InputField>().text = "";
    }

    public void WipeAllInputs(GameObject menu) // Wipes all inputs on all rows
    {
        int[] rowChildIndex;
        rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            WipeInput(tempRow);
        }
    }
    
}
