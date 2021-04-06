using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public GameObject sellMenu; public GameObject buyMenu; public GameObject gameHandler; public GameObject player;
    public int numRowsInMenu = 4; public int numColInMenu = 4;

    void Start()
    {
        Debug.Log("ItemMenu.Start");

        // Testing below functions
        int[] rowChildIndex = new int[numRowsInMenu];
        rowChildIndex = FindRowsWithinMenu(sellMenu);
        
        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = sellMenu.transform.GetChild(rowChildIndex[i]).gameObject;
            string[] rowData = new string[numColInMenu];
            rowData = ReadRow(tempRow);

            for (int x = 0; x < rowData.Length; x++)
            {
                Debug.Log(rowData[x]);
            }

            WriteRow(tempRow, "Test", 1.5f, 100);
        }

        WriteFromDBToMenu("3", sellMenu);
        // End of Testing
    }

    public void UIDetection() // Needs to take the last target from public variable in the player movement script || DO NOT FORGET TO GET RID OF SETACTIVE City UI on OPEN UI Button
    {
        Transform currentlocation = player.GetComponent<PlayerMovement>().lastTarget;

        if (currentlocation.tag == "Shops") // Takes the tag to open correct menu, then gets the data from DB and then writes it to the menu
        {
            // Make Shop UI Active
            // Write data to shop ui
        }
        else if (currentlocation.tag == "Cities")
        {

        }
        else { }
        
    }

    void WriteFromDBToMenu(string locIndex, GameObject menu)
    {
        string[,] arrMenuData = new string[4, 5]; int[] rowChildindex = new int[4]; string[,] getProduct = new string[1, 6];
        arrMenuData = gameHandler.GetComponent<DBconnector>().DBPDLTSelect("LocationID", locIndex);
        rowChildindex = FindRowsWithinMenu(menu);

        for (int i = 0; i < 4; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildindex[i]).gameObject;
            getProduct = gameHandler.GetComponent<DBconnector>().DBPDSelect("ProductID", arrMenuData[i, 0]);
            string itemString = getProduct[0, 1];
            float priceFloat = float.Parse(arrMenuData[i, 3]);
            int stockInt = int.Parse(arrMenuData[i, 2]);

            WriteRow(tempRow, itemString, priceFloat, stockInt);
        }
    }

    public void ResolveSell(GameObject menu)
    {
        int[] rowChildIndex = new int[numRowsInMenu];
        string[] rowElements = new string[numColInMenu];

        rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            rowElements = ReadRow(tempRow);
            string itemName = rowElements[0];
            float itemPrice = float.Parse(rowElements[1]);
            int itemStockInInventory = int.Parse(rowElements[2]);
            int itemToSell = int.Parse(rowElements[3]);

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
            else
            {
                int revenue = Convert.ToInt32(Math.Round(itemToSell * itemPrice));
                itemStockInInventory -= itemToSell;
                WriteRow(tempRow, itemName, itemPrice, itemStockInInventory);
                WipeInput(tempRow);
                gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(revenue, true);
            }

            WipeInput(tempRow);
        }
    }

    public void ResolveBuy()
    {

    }

    int[] FindRowsWithinMenu(GameObject menu) // Finds the child index of any game objects with the first 3 letters containing 'Row' and then returns an array of indexes
    {
        int menuChildCount = menu.transform.childCount;
        int[] rowChildIndex = new int[numRowsInMenu];
        int currentIndex = 0;

        for (int i = 0; i < menuChildCount; i++)
        {
            GameObject tempChild = menu.transform.GetChild(i).gameObject;

            if (tempChild.transform.name.Substring(0, 3) == "Row")
            {
                rowChildIndex[currentIndex] = i;
                currentIndex += 1;
            }
            else { }

        }

        return rowChildIndex;
    }

    string[] ReadRow(GameObject row) // Reads the item, price, stock and input values from the shop or city menu and then outputs them as an array of strings
    {
        string[] rowElements = new string[numColInMenu];
        
        GameObject item = row.transform.GetChild(0).gameObject;
        TextMeshProUGUI itemTMP = item.GetComponent<TextMeshProUGUI>();
        string itemString = itemTMP.text;

        GameObject price = row.transform.GetChild(1).gameObject;
        TextMeshProUGUI priceTMP = price.GetComponent<TextMeshProUGUI>();
        float priceFloat = float.Parse(priceTMP.text);

        GameObject stock = row.transform.GetChild(2).gameObject;
        TextMeshProUGUI stockTMP = stock.GetComponent<TextMeshProUGUI>();
        int stockInt = int.Parse(stockTMP.text);

        GameObject input = row.transform.GetChild(3).gameObject;
        TMP_InputField inputTMPI = input.GetComponent<TMP_InputField>();
        string inputString = inputTMPI.text;

        rowElements[0] = itemString;
        rowElements[1] = Convert.ToString(priceFloat);
        rowElements[2] = Convert.ToString(stockInt);
        rowElements[3] = inputString;

        return rowElements;
    }

    void WriteRow(GameObject row, string itemString, float priceFloat, int stockInt) // Writes to the shop or city menus
    {
        GameObject item = row.transform.GetChild(0).gameObject;
        TextMeshProUGUI itemTMP = item.GetComponent<TextMeshProUGUI>();
        itemTMP.text = itemString;

        GameObject price = row.transform.GetChild(1).gameObject;
        TextMeshProUGUI priceTMP = price.GetComponent<TextMeshProUGUI>();
        priceTMP.text = Convert.ToString(priceFloat);

        GameObject stock = row.transform.GetChild(2).gameObject;
        TextMeshProUGUI stockTMP = stock.GetComponent<TextMeshProUGUI>();
        stockTMP.text = Convert.ToString(stockInt);
    }

    void WipeInput(GameObject row)
    {
        GameObject input = row.transform.GetChild(3).gameObject;
        TMP_InputField inputTMPI = input.GetComponent<TMP_InputField>();
        inputTMPI.text = "";
    }

    public void WipeAllInputs(GameObject menu)
    {
        int[] rowChildIndex = new int[numRowsInMenu];
        rowChildIndex = FindRowsWithinMenu(menu);

        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = menu.transform.GetChild(rowChildIndex[i]).gameObject;
            WipeInput(tempRow);
        }
    }
    
}
