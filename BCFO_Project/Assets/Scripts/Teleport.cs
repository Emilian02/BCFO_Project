using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            Debug.Log("TP");

            if (gameObject.name == "Point1 (1/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-45.7f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            else if (gameObject.name == "Point1 (2/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-61.9f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
        }
    }
}