﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // GameHandler
    public GameObject gameHandler; public GameObject row;

    // Menu's
    public GameObject canvasBackground; public GameObject shopUI; public GameObject cityUI; public GameObject escMenu; public GameObject moneyDisplay;

    void Start()
    {
        Debug.Log("KeyboardControls.Start");
    }

    void Update()
    {
        if (Input.GetKey("j")) // Adds 100 money
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, true);
        }
        else { }
        if (Input.GetKey("h")) // Subtracts 100 money
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, false);
        }
        if (Input.GetKey("k")) // Increases in game time by 1 hour
        {
            gameHandler.GetComponent<InGameTime>().UpdateTime(1);
        }
        else { }
        if (Input.GetKeyDown("l"))
        {
            Debug.Log(gameHandler.GetComponent<InGameTime>().GetTimeInHours());
        }
        else { }
        if (Input.GetKeyDown("q")) // Activates shop menu
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            shopUI.SetActive(!shopUI.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("w")) // Activates city menu
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            cityUI.SetActive(!cityUI.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("e")) // activates escape menu
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            escMenu.SetActive(!escMenu.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("r")) // activates moneydisplay
        {
            moneyDisplay.SetActive(!moneyDisplay.activeSelf);
        }
        else { }
    }
}
