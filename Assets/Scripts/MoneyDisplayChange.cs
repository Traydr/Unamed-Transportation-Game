using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplayChange : MonoBehaviour
{
    public Text money;

    void Start()
    {
        Debug.Log("MoneyDisplayChange.Start");
        money.text = "2000"; // starting amount of money
    }

    public void MoneyChange(int amount, bool postiveValue)
    {
        int currentMoney = ReadMoney();
        if (postiveValue == true)
        {
            currentMoney += amount;
        }
        else
        {
            currentMoney -= amount;
        }
        WriteMoney(currentMoney);
    }

    int ReadMoney()
    {
        int currentMoney = int.Parse(money.text);
        return currentMoney;
    }

    void WriteMoney(int moneyInput)
    {
        money.text = moneyInput.ToString();
    }
}
