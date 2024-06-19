using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform pPosition;
    [SerializeField] private DetectionZone attackZone;

    public float timer;

    void Update()
    {
        if(attackZone.detectedCols.Count > 0 )
        {
            timer += Time.deltaTime;
            if(timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectile, pPosition.position, Quaternion.identity);
    }
}
