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
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private Animator animator;

    


    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void Update()
    {
        if(attackZone.detectedCols.Count > 0)
        {
            AttackPattern();
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

    private void Walking()
    {
        animator.SetBool("isWalking", true);
    }

    private void AttackPattern()
    {
        animator.SetBool("hasTarget", true);
        int attack = Random.Range(1, 3);

        animator.SetInteger("Attack", attack);
    }

   

    private void AwayAttackZone()
    {
        animator.SetBool("hasTarget", false);
    }
}
