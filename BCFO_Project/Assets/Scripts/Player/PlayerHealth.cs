using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;

    void Start()
    {
        currenthealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth < health)
        {
            currenthealth = health;

        }
        if (health <= 0)
        {
            Debug.Log("L bozo");
        }
    }
}
