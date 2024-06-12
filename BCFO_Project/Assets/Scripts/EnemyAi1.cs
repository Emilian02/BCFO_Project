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
    [Header("Pathfinding")]
    [SerializeField] private Transform target;
    [SerializeField] private float activeDistance = 50f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;
    [SerializeField] private LayerMask layerMask;

    [Header("Physics")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpModifier = 0.3f;

    [Header("Custom Behavior")]
    [SerializeField] private bool followEnable = true;
    [SerializeField] private bool jumpEnable = true;
    [SerializeField] private bool directionLookEnable = true;
    [SerializeField] private Animator animator;
    [SerializeField] private DetectionZone attackZone;


    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    bool canMove = true;
    Seeker seeker;
    Rigidbody2D rb;



    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0.0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnable)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnable && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        //Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //See if colliding with anything
        isGrounded = Physics2D.Raycast(transform.position, Vector3.down, 1.2f, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.red);

        //Direction calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Jump
        if (jumpEnable && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        if(attackZone.detectedCols.Count > 0)
        {
            AttackPattern();
        }
        else
        {
            AwayAttackZone();
        }

        //Movement
        if(canMove)
        {
            rb.AddForce(force);
        }

        if (rb.velocity.magnitude != 0)
        {
            Walking();
        }


        //Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Direction Graphics Handling
        if (directionLookEnable)
        {
            if (rb.velocity.x < 0.05f)
            {
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            }
            else if (rb.velocity.x > -0.05f)
            {
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activeDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
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
        canMove = false;
        animator.SetInteger("Attack", attack);
    }

    private void AwayAttackZone()
    {
        animator.SetBool("hasTarget", false);
        canMove = true;
    }
}
