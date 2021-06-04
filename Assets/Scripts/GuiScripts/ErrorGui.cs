using UnityEngine;

public class ErrorGui : MonoBehaviour
{
    public string errorMessage = ""; public Transform selfObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ErrorGui.Start");
    }
    
    // Defines the rectangles of the window and the button
    public Rect windowRect = new Rect(400, 200, 300, 100);
    public Rect buttonRect = new Rect(10, 20, 275, 40);

    void OnGUI() // Is called every frame and displays the below GUI elements
    {
        // Creates the window
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Error");
    }

    // Creates the contents of the window
    void DoMyWindow(int windowId)
    {
        // If the button is pressed then the error is acknowledged and this GUI script is disabled
        if (GUI.Button(buttonRect, errorMessage))
        {
            string toPrint = $"Confirmed received error | ERROR: {errorMessage}";
            print(toPrint);
            selfObject.GetComponent<ErrorGui>().enabled = !selfObject.GetComponent<ErrorGui>().enabled;
        }
    }
    
}
