﻿using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickDetection : MonoBehaviour
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MouseClickDetection.Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string tempTag = hit.transform.tag;
                string test = hit.transform.name;
                switch (tempTag)
                {
                    case "Shops":
                        Debug.Log("HIT SHOP " + test);
                        PMovement(hit.transform);
                        break;
                    case "Cities":
                        Debug.Log("HIT CITY " + test);
                        PMovement(hit.transform);
                        break;
                    default:
                        Debug.Log("Did not hit A city or shop");
                        break;

                }
            }
            else { Debug.Log("No_Click"); }
        }
    }

    void PMovement(Transform target)
    {
        Vector3 targetPos = new Vector3(target.position.x + 10, target.position.y, 1);
        player.position = targetPos;
    }
}
