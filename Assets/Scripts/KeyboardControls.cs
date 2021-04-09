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
        if (Input.GetKey("j")) // Adds 100 money
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, true);
        }
        if (Input.GetKey("h")) // Subtracts 100 money
        {
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(100, false);
        }
        if (Input.GetKey("k")) // Increases in game time by 1 hour
        {
            gameHandler.GetComponent<InGameTime>().UpdateTime(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Toggles escape menu
        {
            canvasBackground.SetActive(true);
            escMenu.SetActive(!escMenu.activeSelf);
        }
        if (Input.GetKeyDown("m"))
        {
            for (int i = 0; i < 6; i++)
            {
                Debug.Log(gameHandler.GetComponent<DBconnector>().DBPLSelect("ProductID", i.ToString()));
            }
        }
    }
}
