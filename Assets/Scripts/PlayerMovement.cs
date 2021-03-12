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
    public GameObject MoneyChange;

    void Start()
    {
        Debug.Log("PlayerMovement.Start");
        PMovement(initialStart);
    }

    public void PMovement(Transform target)
    {
        bool connectionValid = FindConnectionBetweenTwoLocations(FindIndexOfLocation(lastTarget.transform.name), FindIndexOfLocation(target.transform.name));
        
        if (connectionValid == true)
        {
            Vector3 targetPos = new Vector3(target.position.x + 10, target.position.y, 1);
            player.position = targetPos;
            MoneyChange.GetComponent<MoneyDisplayChange>().MoneyChange(CalcFeulCost(lastTarget, target), false);
            lastTarget = target;
        }
        else if (target == initialStart)
        {
            Vector3 targetPos = new Vector3(target.position.x + 10, target.position.y, 1);
            player.position = targetPos;
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
        {0, 1, 0, 1, 0, 0}, // C, C1, C2, S, S1, S2
        {1, 0, 0, 0, 1, 0}, // C1
        {0, 0, 0, 1, 1, 0}, // C2
        {1, 0, 1, 0, 0, 1}, // S
        {0, 1, 1, 0, 0, 0}, // S1
        {0, 0, 0, 1, 0, 0}  // s2
        }; // 2d array of all connections between locations

        if (locationConnectionArray[indexinitial, indexTarget] == 1)
        {
            connectionFound = true;
        }
        else { }

        return connectionFound;
    }

    // To be decided if this should remain here
    // Something is going very wrong here and I dont know how to correct it right now
    int CalcFeulCost(Transform initialPos, Transform targetPos)
    {
        int feulCost = 0;
        int iPosX = Convert.ToInt32(initialPos.position.x); int iPosY = Convert.ToInt32(initialPos.position.y);
        int tPosX = Convert.ToInt32(targetPos.position.x); int tPosY = Convert.ToInt32(targetPos.position.y);
        float deltaX = tPosX - iPosX; float deltaY = tPosY - iPosY;

        float tempFeul = math.round(math.sqrt(deltaX * deltaX + deltaY * deltaY));
        feulCost = Convert.ToInt32(tempFeul);
        return feulCost;
    }
}