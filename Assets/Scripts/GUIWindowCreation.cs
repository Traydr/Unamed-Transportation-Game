using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWindowCreation : MonoBehaviour
{
    public string errorMessage = ""; public Transform selfObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GUIWindowCreation.Start");
    }
    
    public Rect windowRect = new Rect(20, 20, 120, 50);
    public Rect buttonRect = new Rect(10, 10, 150, 20);

    void OnGUI()
    {
        // Register the window. Notice the 3rd parameter
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Error");
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(buttonRect, errorMessage))
        {
            string toPrint = string.Format("Confirmed recieved error | ERROR: {0}", errorMessage);
            print(toPrint);
            selfObject.GetComponent<GUIWindowCreation>().enabled = !selfObject.GetComponent<GUIWindowCreation>().enabled;
        }
    }
    
}
