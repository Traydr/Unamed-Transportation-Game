using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public Text timeDisplay;


    // Time format {0}D {1}H
    void Start()
    {
        Debug.Log("Time.Start");

        // Testing Below
        int[] dayHour = new int[2];
        UpdateTime(25);
        dayHour = GetTime();
        string tempLog = string.Format("{0} days, {1} hours", dayHour[0], dayHour[1]);
        Debug.Log(tempLog);

    }

    void UpdateTime(int advanceTimeByNumHours)
    {
        int newDayValue = 0; int newHourValue = 0; int[] storedTime = new int[2];
        storedTime = GetTime(); newHourValue = storedTime[1]; newHourValue = storedTime[0];
        newHourValue += advanceTimeByNumHours;

        while (newHourValue > 24)
        {
            newHourValue -= 24;
            newDayValue += 1;
        }

        timeDisplay.text = string.Format("{0}D {1}H", newDayValue, newHourValue);
    }

    int[] GetTime() // Error here: Input string was not in a correct format.
    {
        string[] currentTime = timeDisplay.text.Split(' ');
        int[] storedTime = new int[2];

        for (int i = 0; i < currentTime.Length; i++)
        {
            storedTime[i] = int.Parse(currentTime[i].Substring(0,currentTime[i].Length - 1));
        }

        return storedTime;
    }
}
