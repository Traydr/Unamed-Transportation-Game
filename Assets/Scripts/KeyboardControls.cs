using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // GameHandler
    public GameObject gameHandler;
    
    void Start()
    {
        Debug.Log("KeyboardControls.Start");
    }

    void FixedUpdate()
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
    }
}
