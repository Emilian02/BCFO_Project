using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using Pathfinding.Util;
using Unity.VisualScripting;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class EnemyAi1 : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private Animator animator;
    [Header("HIT BOXES")]
    [SerializeField] private GameObject punchPoint;
    [SerializeField] private GameObject kickPoint;
    [SerializeField] private float radius;
    public bool hurt = false;
    public LayerMask players;

    public float timer = 0.0f;
    private int attackPattern;




    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void Update()
    {
        if(health.health <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("hasTarget", true);
            movement.enabled = false;
            DeadTimer();
        }
        else if(hurt)
        {
            movement.enabled = false;
            animator.SetBool("isHurt", true);
            StuntTimer();
        }
        else
        {
            if(attackZone.detectedCols.Count > 0)
            {
                attackPattern = Random.Range(1, 3);
                AttackPattern(attackPattern);
            }
            else
            {
                AwayAttackZone();
            }


            if (movement.rb.velocity.x < -0.1 || movement.rb.velocity.x > 0.1)
            {
                Walking();
            }
        }

    }

    public void punch()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(punchPoint.transform.position, radius, players);

        foreach (Collider2D playerGameobject in player)
        {
            PlayerMovement.slide = true;
            Debug.Log("Enemy hit you");
            playerGameobject.GetComponent<PlayerHealth>().health -= 5;
            playerGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * -3f, ForceMode2D.Impulse);

        }
    }
    public void kick()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(kickPoint.transform.position, radius, players);

        foreach (Collider2D playerGameobject in player)
        {
            PlayerMovement.slide = true;
            Debug.Log("Enemy hit you");
            playerGameobject.GetComponent<PlayerHealth>().health -= 10;
            playerGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * -5f, ForceMode2D.Impulse);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.transform.position, radius);
        Gizmos.DrawWireSphere(kickPoint.transform.position, radius);
    }

    private void Walking()
    {
        animator.SetBool("isWalking", true);
    }

    private void AttackPattern(int attack)
    {
        animator.SetBool("hasTarget", true);

        animator.SetInteger("Attack", attack);
    }

    private void AwayAttackZone()
    {
        animator.SetBool("hasTarget", false);
    }

    private void StuntTimer()
    {
        timer += Time.deltaTime;
        if(timer >= 1.5f)
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
