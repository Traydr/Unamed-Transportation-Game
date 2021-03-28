using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public GameObject row0; public GameObject row1; public GameObject row2; public GameObject row3; public GameObject sellMenu;
    void Start()
    {
        Debug.Log("ItemMenu.Start");

        // Testing below functions
        int[] rowChildIndex = new int[4];
        rowChildIndex = FindRowsWithinMenu(sellMenu);
        
        for (int i = 0; i < rowChildIndex.Length - 1; i++)
        {
            GameObject tempRow = sellMenu.transform.GetChild(rowChildIndex[i]).gameObject;
            string[] rowData = new string[4];
            rowData = ReadRow(tempRow);

            for (int x = 0; x < rowData.Length - 1; x++)
            {
                Debug.Log(rowData[x]);
            }

            WriteRow(tempRow, "Test", 1.5f, 100);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            string[] rowData = new string[4];
            rowData = ReadRow(row0);

            for (int x = 0; x < rowData.Length - 1; x++)
            {
                Debug.Log(rowData[x]);
            }
        }
        else { }
        if (Input.GetKeyDown("o"))
        {
            WriteRow(row0, "Hello", 12.3f, 123);
        }
        else { }
    }

    int[] FindRowsWithinMenu(GameObject menu)
    {
        int menuChildCount = menu.transform.childCount;
        int[] rowChildIndex = new int[4];

        for (int i = 0; i < menuChildCount - 1; i++)
        {
            GameObject tempChild = menu.transform.GetChild(i).gameObject;
            int currentIndex = 0;

            if (tempChild.transform.name.Substring(0,2) == "row")
            {
                rowChildIndex[currentIndex] = i;
            }
        }

        return rowChildIndex;
    }

    string[] ReadRow(GameObject row) // Should probably have an output of an array!
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
        int inputString = int.Parse(inputTMPI.text);

        rowChildValues[0] = itemString;
        rowChildValues[1] = Convert.ToString(priceFloat);
        rowChildValues[2] = Convert.ToString(stockInt);
        rowChildValues[3] = Convert.ToString(inputString);

        return rowChildValues;
    }

    void WriteRow(GameObject row, string itemString, float priceFloat, int stockInt)
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
