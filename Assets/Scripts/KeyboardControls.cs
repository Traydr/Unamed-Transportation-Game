using System.Collections;
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
        if (Input.GetKey("j"))
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, true);
        }
        else { }
        if (Input.GetKey("h"))
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, false);
        }
        else { }
        //if (Input.GetKey("p"))
        //{
        //    gameHandler.GetComponent<SellMenu>().CalcResult(row, true);
        //}
        //else { }
        if (Input.GetKeyDown("q"))
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            shopUI.SetActive(!shopUI.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("w"))
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            cityUI.SetActive(!cityUI.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("e"))
        {
            canvasBackground.SetActive(!canvasBackground.activeSelf);
            escMenu.SetActive(!escMenu.activeSelf);
        }
        else { }
        if (Input.GetKeyDown("r"))
        {
            moneyDisplay.SetActive(!moneyDisplay.activeSelf);
        }
        else { }
    }
}
