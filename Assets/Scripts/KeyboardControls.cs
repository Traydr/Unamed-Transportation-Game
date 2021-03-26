using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // Money
    public GameObject moneyChange;

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
            moneyChange.GetComponent<MoneyDisplayChange>().MoneyChange(100, true);
        }
        else { }
        if (Input.GetKey("h"))
        {
            moneyChange.GetComponent<MoneyDisplayChange>().MoneyChange(100, false);
        }
        else { }
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
