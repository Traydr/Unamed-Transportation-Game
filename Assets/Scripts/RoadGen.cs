using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.U2D.Animation;
using UnityEngine;

/* This script is intended to only ever run once when the game is running / when a save file is loaded
 * Activate or deactivate pre-made roads. Changing the 2d array of raods that currently exists inside Playermovement.cs
 */
public class RoadGen : MonoBehaviour
{
    public Transform loc1; public Transform loc2;

    void Start()
    {
        
    }
}