using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickDetection : MonoBehaviour
{
    public Transform player; public Transform selfObject;
    
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
            Debug.Log("Registered_Click_1");

            if (Physics.Raycast(ray, out hit))
            {
                string tempTag = hit.transform.tag;
                Debug.Log("Registered_Click_2");
                switch (tempTag)
                {
                    case "Shops":
                        break;
                    case "Cities":
                        break;
                    default:
                        Debug.Log("Did not hit A city or shop");
                        break;

                }
            }
            else { Debug.Log("No_Click"); }
        }
    }
}
