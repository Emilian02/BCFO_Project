using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    public List<Collider2D> detectedCols = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            detectedCols.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCols.Remove(collision);
    }
}
