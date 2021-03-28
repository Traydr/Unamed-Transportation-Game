using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickDetection : MonoBehaviour
{
    public Transform player; public GameObject playerM;

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

                playerM.GetComponent<PlayerMovement>().PMovement(hit.transform);

                switch (tempTag)
                {
                    case "Shops":
                        Debug.Log("HIT SHOP " + test);
                        break;
                    case "Cities":
                        Debug.Log("HIT CITY " + test);
                        break;
                    default:
                        Debug.Log("Did not hit A city or shop");
                        break;

                }
            }
            else { }
        }
    }
}