using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player; public Transform initialStart; public Transform lastTarget;

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
            lastTarget = target;
        }
        else { }
    }

    private int FindIndexOfLocation(string targetName)
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

    private bool FindConnectionBetweenTwoLocations(int indexinitial, int indexTarget)
    {
        bool connectionFound = false;
        int[,] locationConnectionArray = new int[,] 
        {
        {0, 1, 0, 1, 0, 0},
        {1, 0, 0, 0, 1, 0},
        {0, 0, 0, 1, 1, 0},
        {1, 0, 1, 0, 0, 1},
        {0, 1, 1, 0, 0, 0},
        {0, 0, 0, 1, 0, 0} 
        }; // 2d array of all connections between locations

        if (locationConnectionArray[indexinitial, indexTarget] == 1)
        {
            connectionFound = true;
        }
        else { }

        return connectionFound;
    }
}