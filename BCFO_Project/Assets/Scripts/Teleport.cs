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
            if (gameObject.name == "Point1 (2/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-61.9f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            if (gameObject.name == "Point2 (1/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-304.1f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            if (gameObject.name == "Point2 (2/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-323.1f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }

            if (gameObject.name == "Point3 (1/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-173.39f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            if (gameObject.name == "Point3 (2/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-194.1f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            if (gameObject.name == "Point4 (1/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-434.74f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
            if (gameObject.name == "Point4 (2/2)" && PlayerMovement.canTeleport == true)
            {
                hit.gameObject.transform.position = new Vector3(-511.6f, -2.925796f, 0);
                PlayerMovement.canTeleport = false;
            }
        }
    }
}