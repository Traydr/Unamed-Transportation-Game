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

    // Addition for UpdateTime, each time the loop is done, an event script should be called to evaluate the day !!
    public void UpdateTime(int advanceTimeByNumHours) // Takes in a number of hours to add to the current time, then it correctly calculates how it should be displayed and dsiplays it
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

    public void SetTime(int dayValue, int hourValue) // Changes the time displayed on the game screen
    {
        timeDisplay.text = string.Format("{0}D {1}H", dayValue, hourValue);
    }

    public void SetTimeFromTotalHours(int totalHours) // Takes a number of hours and displays it as time elapsed
    {
        SetTime(0, 0);
        UpdateTime(totalHours);
    }

    public int GetTimeInHours() // Takes the  time displayed and returns the time in hours
    {
        int totalHours = 0; int[] currentTime = new int[2];
        currentTime = GetTime();

        totalHours = (currentTime[0] * 24) + (currentTime[1]);

        return totalHours;
    }
}
