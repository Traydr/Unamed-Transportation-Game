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
        Debug.Log("MoneyDisplayChange Initited");
        money.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            int currentMoney = int.Parse(money.text);
            currentMoney += 100;
            money.text = currentMoney.ToString();
        }
        else { }
    }
}
