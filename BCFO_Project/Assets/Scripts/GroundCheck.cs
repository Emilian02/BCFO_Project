using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform") // || collision.gameObject.tag == "Player")
        {
            playerMovement.LandedOnPlatform();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")//|| collision.gameObject.tag == "Player")
        {
            playerMovement.FellOffPlatform();
        }
    }
}