using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.U2D.Animation;
using UnityEngine;

/* This script is intended to only ever run once when the game is running / when a save file is loaded
 * Generate black rectangles between 2 points.
 * New plan: enable or disable manually placed roads depending on an excel file
 */
public class RoadGen : MonoBehaviour
{
    public Transform loc1; public Transform loc2;

    void Start()
    {
        
    }
}