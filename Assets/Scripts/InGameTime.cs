using UnityEngine;
using UnityEngine.UI;

public class InGameTime : MonoBehaviour
{
    public Text timeDisplay; public GameObject gameHandler;


    // Time format {0}D {1}H
    void Start()
    {
        Debug.Log("Time.Start");
    }

    // Takes in the number of hours the in game time should be advanced by
    // It gets the data and then adds the hours to the current hours and runs a while loop to make sure the hours dont exceed 24
    // If they do then a day is added to the in game time. Then the values are sent to SetTime() function
    public void UpdateTime(int advanceTimeByNumHours)
    {
        int newDayValue = 0; int newHourValue = 0; int[] storedTime = new int[2];
        storedTime = GetTime(); newHourValue = storedTime[1]; newDayValue = storedTime[0];
        newHourValue += advanceTimeByNumHours;

        while (newHourValue > 24)
        {
            newHourValue -= 24;
            newDayValue += 1;
            // MAKE CALL HERE
            gameHandler.GetComponent<GameEventHandler>().CallEventRequest("ELPU");
        }

        SetTime(newDayValue, newHourValue);
    }

    // Get the integer values in time by splitting the text string by the space
    // It then removes the end letter of the numbers and stores them in an array of integers and returns that array
    public int[] GetTime() // Gets the time from display on UI
    {
        string[] currentTime = timeDisplay.text.Split(' ');
        int[] storedTime = new int[2];

        for (int i = 0; i < currentTime.Length; i++)
        {
            storedTime[i] = int.Parse(currentTime[i].Substring(0,currentTime[i].Length - 1));
        }

        return storedTime;
    }

    // Set the time by formating the input data into the string in the correct format, then sets the displayed text to it
    public void SetTime(int dayValue, int hourValue)
    {
        timeDisplay.text = string.Format("{0}D {1}H", dayValue, hourValue);
    }

    // Sets the time to 0 and then advances the amount of time by the integer given
    public void SetTimeFromTotalHours(int totalHours)
    {
        SetTime(0, 0);
        UpdateTime(totalHours);
    }

    // Takes the  time displayed and returns the total time in hours
    public int GetTimeInHours() 
    {
        int totalHours = 0; int[] currentTime = new int[2];
        currentTime = GetTime();

        totalHours = (currentTime[0] * 24) + (currentTime[1]);

        return totalHours;
    }
}
