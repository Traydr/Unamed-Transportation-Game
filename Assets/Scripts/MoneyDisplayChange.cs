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

    public void MoneyChange(int amount, bool postiveValue) // Takes an amount of money and wether this is addition or subtraction and does the corresponding action and then changes the money displayed
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

    public int ReadMoney() // Reads the money that is currently displayed in the game
    {
        int currentMoney = int.Parse(money.text);
        return currentMoney;
    }

    public void WriteMoney(int moneyInput) // Writes the money to the text that displays the total money the player has
    {
        money.text = moneyInput.ToString();
    }
}
