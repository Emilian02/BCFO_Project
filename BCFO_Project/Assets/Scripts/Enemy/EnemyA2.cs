using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyA2 : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private EnemyShooting projectile;
    [SerializeField] private Animator animator;

    private float timer = 0;
    public bool hurt = false;

    void Update()
    {
        if(health.health <= 0)
        {
            animator.SetBool("isDead", true);
            movement.enabled = false;
            DeadTimer();
        }
        else if (hurt)
        {
            stunt();
        }
    }

    void stunt()
    {
        timer += Time.deltaTime;
        movement.enabled = false;
        animator.SetBool("isHurt", true) ;
        projectile.timer = 0 ;

        if (timer >= 1.5f)
        {
            hurt = false;
            timer = 0.0f;
            animator.SetBool("isHurt", hurt);
            movement.enabled = true;
        }
    }

    private void DeadTimer()
    {
        timer += Time.deltaTime;
        if (timer >= 2.5f)
        {
            Destroy(gameObject);
        }
    }
}
