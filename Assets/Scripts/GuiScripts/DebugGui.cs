using UnityEngine;

public class DebugGui : MonoBehaviour
{
    public Transform selfObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DebugGui.Start");
    }
    
    // Defines the rectangles of the window and the button
    public Rect windowRect = new Rect(400, 200, 300, 100);
    public Rect buttonRect = new Rect(10, 20, 275, 40);

    void OnGUI() // Is called every frame and displays the below GUI elements
    {
        // Creates the window
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Debug Menu");
    }

    // Creates the contents of the window
    void DoMyWindow(int windowId)
    {
        string buttonDataIn = "";
        if (GUI.Button(buttonRect, buttonDataIn))
        {
            string toPrint = "Closed debug menu";
            print(toPrint);
            selfObject.GetComponent<ErrorGui>().enabled = !selfObject.GetComponent<ErrorGui>().enabled;
        }
    }
}