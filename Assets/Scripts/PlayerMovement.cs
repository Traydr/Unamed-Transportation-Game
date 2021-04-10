using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player; public Transform initialStart; public Transform lastTarget;
    public GameObject gameHandler;

    void Start()
    {
        Debug.Log("PlayerMovement.Start");
        PMovement(initialStart);
    }

    public void PMovement(Transform target) // Checks that the player can move to target and if so it then moves
    {
        bool connectionValid = FindConnectionBetweenTwoLocations(FindIndexOfLocation(lastTarget.transform.name), FindIndexOfLocation(target.transform.name));
        
        if (connectionValid == true)
        {
            Vector3 targetPos = new Vector3(target.position.x + 10, target.position.y, 1);
            player.position = targetPos;
            gameHandler.GetComponent<MoneyDisplayChange>().MoneyChange(CalcFeulCost(lastTarget, target), false);
            gameHandler.GetComponent<InGameTime>().UpdateTime(CalcTimeCost(lastTarget, target));
            lastTarget = target;
        }
        else { }
    }

    private int FindIndexOfLocation(string targetName) // Gets the index of a location and returns the index
    {
        int locationIndex = -1;
        string[] locationIndexArr = new string[] { "City", "City (1)", "City (2)", "Shop", "Shop (1)", "Shop (2)" };

        for (int i = 0; i < locationIndexArr.Length; i++)
        {
            if (locationIndexArr[i] == targetName)
            {
                locationIndex = i;
            }
            else { }
        }
        return locationIndex;
    }

    private bool FindConnectionBetweenTwoLocations(int indexinitial, int indexTarget) // Finds if there is a conection between 2 locations and returns true or false
    {
        bool connectionFound = false;
        int[,] locationConnectionArray = new int[,] 
        {
        {1, 1, 0, 1, 0, 0}, // C, C1, C2, S, S1, S2
        {1, 1, 0, 0, 1, 0}, // C1
        {0, 0, 1, 1, 1, 0}, // C2
        {1, 0, 1, 1, 0, 1}, // S
        {0, 1, 1, 0, 1, 0}, // S1
        {0, 0, 0, 1, 0, 1}  // s2
        }; // 2d array of all connections between locations

        if (locationConnectionArray[indexinitial, indexTarget] == 1)
        {
            connectionFound = true;
        }
        else { }

        return connectionFound;
    }

    // This possibly may be moved to 'Economics.cs'
    int CalcFeulCost(Transform initialPos, Transform targetPos) // Calculates the feul cost of traveling based on the distance travelled
    {
        int feulCost = 0; int multiplier = 1;

        float magnitude = GetMagnitudeBetweenTwoPoints(initialPos, targetPos);
        feulCost = Convert.ToInt32(math.round(magnitude * multiplier));

        return feulCost;
    }

    int CalcTimeCost(Transform initialPos, Transform targetPos) // Calculates the time taken to move between diffrent locations
    {
        int timeCost = 0;

        float magnitude = GetMagnitudeBetweenTwoPoints(initialPos, targetPos);
        timeCost = Convert.ToInt32(math.round(magnitude * 0.1f));

        return timeCost;
    }

    float GetMagnitudeBetweenTwoPoints(Transform initialPos, Transform targetPos) // Gets the distance between 2 locations
    {
        float magnitude = 0f;

        int iPosX = Convert.ToInt32(initialPos.position.x); int iPosY = Convert.ToInt32(initialPos.position.y);
        int tPosX = Convert.ToInt32(targetPos.position.x); int tPosY = Convert.ToInt32(targetPos.position.y);
        float deltaX = tPosX - iPosX; 
        float deltaY = tPosY - iPosY;

        magnitude = math.sqrt(deltaX * deltaX + deltaY * deltaY);

        return magnitude;
    }
}