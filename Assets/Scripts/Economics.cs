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

    //  Uses the price elesticity of demand equation to calculate what the new price would be
    public float CalcChangeInPrice(float ped, float currentPrice, int lastStock, int currentStock) // Takes the PED and the % change in demand and then calculates the resultant price and returns it
    {
        // Ped = % change in Qty Demanded / % change in Price so %CH Price = %CH QD / PED
        float resultantPrice = 0f;
        float percentChangeInQuantityDemanded = GetPercentageChangeInValue(lastStock, currentStock);

        float percentChangeInPrice = percentChangeInQuantityDemanded / ped;
        resultantPrice = currentPrice + (currentPrice * percentChangeInPrice);

        return resultantPrice;
    }

    // Takes 2 values and calculates the percentage change between the values
    float GetPercentageChangeInValue(float initialVal, float endVal) // Gets the percentage from a diffrence of 2 values
    {
        float perChangeInValue = 0f;

        perChangeInValue = (endVal / initialVal);

        return perChangeInValue;
    }
}
