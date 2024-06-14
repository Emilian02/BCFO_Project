using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;
    public bool isBoss;

    void Start()
    {
        currenthealth = health;
    }

    void Update()
    {
        if (currenthealth < health)
        {
            currenthealth = health;

        }
        if (health <= 0 && isBoss == true)
        {
            PlayerMovement.isBossFight = false;
            Debug.Log("Boss Down");
        }
        if (health <= 0 && isBoss == false)
        {
            Debug.Log("Enemy Down");
        }
    }
}
