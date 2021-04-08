using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Economics.Start");
    }

    float CalcChangeInPrice(float ped, float currentPrice, int lastStock, int currentStock) // Takes the PED and the % change in demand and then calculates the resultant price and returns it
    {
        // Ped = % change in Qty Demanded / % change in Price so %CH Price = %CH QD / PED
        float resultantPrice = 0f;
        float perCHQD = GetPercentageChangeInValue(lastStock, currentStock);

        float perCHP = perCHQD / ped;
        resultantPrice = currentPrice + (currentPrice * perCHP);

        return resultantPrice;
    }

    float GetPercentageChangeInValue(float initialVal, float endVal) // Gets the percentage from a diffrence of 2 values
    {
        float perChVal = 0f;

        perChVal = 1 - (endVal / initialVal);

        return perChVal;
    }
}
