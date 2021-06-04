using System;
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

    // Moves the player from 1 location to another
    public void PMovement(Transform target)
    {
        // Checks if the player is able to move the clicked location, by getting the indexes of the locations and then checking it in the FindConnectionBetweenTwoLocations() functions
        bool connectionValid = FindConnectionBetweenTwoLocations(FindIndexOfLocation(lastTarget.transform.name), FindIndexOfLocation(target.transform.name));
        
        // If the connection is valid the following code is executed
        if (connectionValid == true)
        {
            // The Player position goes to the target position with an offset of 10x
            Vector3 targetPos = new Vector3(target.position.x + 10, target.position.y, 1);
            player.position = targetPos;
            
            // The Money is subtracted by the distance moved, the time also changes depending on the distance moved
            gameHandler.GetComponent<GameMoneyHandler>().MoneyChange(CalcFeulCost(lastTarget, target), false);
            gameHandler.GetComponent<GameTimeHandler>().UpdateTime(CalcTimeCost(lastTarget, target));
            
            // The lastTarget becomes the current position where the player is located
            lastTarget = target;
        }
    }

    // Finds the index of targets name by searching for it within an array of strings
    // If its not found an index of -1 is returned | If its found then the correct index is returned
    private int FindIndexOfLocation(string targetName)
    {
        int locationIndex = -1;
        string[] locationIndexArr = { "City", "City (1)", "City (2)", "Shop", "Shop (1)", "Shop (2)" };

        for (int i = 0; i < locationIndexArr.Length; i++)
        {
            if (locationIndexArr[i] == targetName)
            {
                locationIndex = i;
            }
        }
        return locationIndex;
    }

    // Finds if the is a connection between 2 locations by getting their indexes
    // Those indexes are put into a 2d array and if the array has a 1 on the positions then there is a connection
    // If there is a 0 then there isnt a connection
    private bool FindConnectionBetweenTwoLocations(int indexinitial, int indexTarget)
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

        return connectionFound;
    }
    
    // Calculates the feul cost of traveling based on the distance travelled and multiplies it by a multiplier
    int CalcFeulCost(Transform initialPos, Transform targetPos) 
    {
        int feulCost = 0; int multiplier = 1;

        float magnitude = GetMagnitudeBetweenTwoPoints(initialPos, targetPos);
        feulCost = Convert.ToInt32(math.round(magnitude * multiplier));

        return feulCost;
    }

    // Calculates the time taken to move between diffrent locations depending on the distance 
    int CalcTimeCost(Transform initialPos, Transform targetPos) 
    {
        int timeCost = 0;

        float magnitude = GetMagnitudeBetweenTwoPoints(initialPos, targetPos);
        timeCost = Convert.ToInt32(math.round(magnitude * 0.1f));

        return timeCost;
    }

    // Gets the magnitude of the distance between 2 locations by using Pythagoras's theorem
    float GetMagnitudeBetweenTwoPoints(Transform initialPos, Transform targetPos) 
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