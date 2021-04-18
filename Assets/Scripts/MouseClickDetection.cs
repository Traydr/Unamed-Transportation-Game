﻿using UnityEngine;

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
        if (Input.GetMouseButtonDown(0)) // If the left mouse button is pressed then this action is executed
        {
            // A physics ray is created and shot where the mouse position was
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If there is a hit then transform of whatever was hit is inputted into the PMovement() function
            if (Physics.Raycast(ray, out hit))
            {
                string tempTag = hit.transform.tag;
                string test = hit.transform.name;

                playerM.GetComponent<PlayerMovement>().PMovement(hit.transform);
            }
        }
    }
}