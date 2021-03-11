using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplayChange : MonoBehaviour
{
    public Text money;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MoneyDisplayChange.Start");
        money.text = "2000";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            MoneyChange(100, true);
        }
        else { }
    }

    public void MoneyChange(int amount, bool postiveValue)
    {
        int currentMoney = int.Parse(money.text);
        if (postiveValue == true)
        {
            currentMoney += amount;
        }
        else
        {
            currentMoney -= amount;
        }
        money.text = currentMoney.ToString();
    }
}
