using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.U2D.Animation;
using UnityEngine;

/* This script is intended to only ever run once when the game is running / when a save file is loaded
 * Generate black rectangles between 2 points.
 * 
 */
public class RoadGen : MonoBehaviour
{
    public Transform loc1; public Transform loc2;

    void Start()
    {
        
    }

    private void OnGUI()
    {
        // This doesnt do what I wanted it to do and ive made the decision to manually create the roads.
        Debug.Log("RoadGen OnGUI initiated");

        float weightMultiplier = 10f;
        float positionX = (loc1.position.x - loc2.position.x) * weightMultiplier; 
        float positionY = (loc1.position.y - loc2.position.y) * weightMultiplier; 
        float sizeX = 100; float sizeY = 100;
        
        Texture2D MyTexture =  new Texture2D(128, 128);
        GUI.color = new Color(1.0f, 0, 0);
        GUI.DrawTexture(new Rect(positionX, positionY, sizeX, sizeY), MyTexture, ScaleMode.ScaleToFit, true, 10f);
    }
}