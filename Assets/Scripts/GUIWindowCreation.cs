﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWindowCreation : MonoBehaviour
{
    // Dont know what I want to do with this yet. debug?
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GUIWindowCreation.Start");
    }
    
    public Rect windowRect = new Rect(20, 20, 120, 50);

    void OnGUI()
    {
        // Register the window. Notice the 3rd parameter
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        {
            print("Got a click");
        }
    }
    
}
