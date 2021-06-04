using System;
using UnityEngine;

/* This script is intended to only ever run once when the game is running / when a save file is loaded
 * Activate or deactivate pre-made roads. Changing the 2d array of raods that currently exists inside Playermovement.cs
 */
public class RoadGeneration : MonoBehaviour
{
    public Transform loc1; public Transform loc2;

    void Start()
    {
        Console.WriteLine("RoadGeneration.Start");
    }
}