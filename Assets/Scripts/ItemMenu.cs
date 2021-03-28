using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public GameObject sellMenu;

    void Start()
    {
        Debug.Log("ItemMenu.Start");

        // Testing below functions
        int[] rowChildIndex = new int[4];
        rowChildIndex = FindRowsWithinMenu(sellMenu);
        
        for (int i = 0; i < rowChildIndex.Length; i++)
        {
            GameObject tempRow = sellMenu.transform.GetChild(rowChildIndex[i]).gameObject;
            string[] rowData = new string[4];
            rowData = ReadRow(tempRow);

            for (int x = 0; x < rowData.Length; x++)
            {
                Debug.Log(rowData[x]);
            }

            WriteRow(tempRow, "Test", 1.5f, 100);
        }
    }

    int[] FindRowsWithinMenu(GameObject menu) // Finds the child index of any game objects with the first 3 letters containing 'Row' and then returns an array of indexes
    {
        int menuChildCount = menu.transform.childCount;
        int[] rowChildIndex = new int[4];
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
        string[] rowChildValues = new string[4];
        
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

        rowChildValues[0] = itemString;
        rowChildValues[1] = Convert.ToString(priceFloat);
        rowChildValues[2] = Convert.ToString(stockInt);
        rowChildValues[3] = inputString;

        return rowChildValues;
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
    
}
