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
    [Header("Float")]
    [SerializeField] private float rollSpeed;

    Rigidbody2D rb;
    bool hasTarget = false;
    float attackTimer = 0.0f;
    public bool hurt = false;
    public float timer = 0.0f;

    private enum AttackStates
    { 
        preapring, attacking, ending
    };

    AttackStates attackstate;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
        rb = GetComponent<Rigidbody2D>();
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
                hasTarget = true;
            }

            if (hasTarget)
            {
                if (attackstate == AttackStates.preapring)
                {
                    StartAttack();
                }
                if (attackstate == AttackStates.attacking)
                {
                    Attack();
                }
                if (attackstate == AttackStates.ending)
                {
                    FinishAttack();
                }
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

    private void StartAttack()
    {
        animator.SetBool("hasTarget", true);
        animator.SetBool("preparing", true);
        movement.enabled = false;
        attackTimer += Time.deltaTime;

        if (attackTimer >= 1.5f)
        {
            animator.SetBool("preparing", false);
            attackTimer = 0.0f;
            attackstate = AttackStates.attacking;
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 0.5f)
        {
            rb.AddForce(transform.right * rollSpeed, ForceMode2D.Impulse);
            animator.SetBool("finishAttack", true);
            attackTimer = 0.0f;
            attackstate = AttackStates.ending;
        }
    }

    private void FinishAttack()
    {
        attackTimer += Time.deltaTime;
        if( attackTimer >= 1f)
        {
            animator.SetBool("hasTarget", false);
            attackTimer = 0.0f;
            animator.SetBool("finishAttack", false);
            attackstate = AttackStates.preapring;
            movement.enabled = true;
            hasTarget = false;
        }
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
