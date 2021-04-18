using UnityEngine;

public class GUIWindowCreation : MonoBehaviour
{
    public string errorMessage = ""; public Transform selfObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GUIWindowCreation.Start");
    }
    
    // Defines the rectangles of the window and the button
    public Rect windowRect = new Rect(20, 20, 120, 50);
    public Rect buttonRect = new Rect(10, 10, 150, 20);

    void OnGUI() // Is called every frame and displays the below GUI elements
    {
        // Creates the window
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Error");
    }

    // Creates the contents of the window e.g. the button
    void DoMyWindow(int windowID)
    {
        // If the button is pressed then the error is acknowledged and this GUI script is disabled
        if (GUI.Button(buttonRect, errorMessage))
        {
            string toPrint = string.Format("Confirmed recieved error | ERROR: {0}", errorMessage);
            print(toPrint);
            selfObject.GetComponent<GUIWindowCreation>().enabled = !selfObject.GetComponent<GUIWindowCreation>().enabled;
        }
    }
    
}
