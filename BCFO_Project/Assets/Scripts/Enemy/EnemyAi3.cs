using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi3 : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private Animator animator;

    public bool hurt = false;
    public float timer = 0.0f;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    void Update()
    {
        if (health.health <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("hasTarget", true);
            movement.enabled = false;
            DeadTimer();
        }
        else if (hurt)
        {
            movement.enabled = false;
            animator.SetBool("isHurt", true);
            StuntTimer();
        }
        else
        {
            if (attackZone.detectedCols.Count > 0)
            {
                
            }
            else
            {
                AwayAttackZone();
            }

            if (movement.rb.velocity.x != 0)
            {
                Walking();
            }
        }
    }

    private void Walking()
    {
        animator.SetBool("isWalking", true);
    }

    private void AwayAttackZone()
    {
        animator.SetBool("hasTarget", false);
    }

    private void StuntTimer()
    {
        timer += Time.deltaTime;
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
