using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private Transform target;
    [SerializeField] private float activeDistance = 50f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;
    [SerializeField] private LayerMask layerMask;

    [Header("Physics")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private float jumpSpeed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpModifier = 0.3f;

    [Header("Custom Behavior")]
    [SerializeField] private bool followEnable = true;
    [SerializeField] private bool jumpEnable = true;
    [SerializeField] private bool directionLookEnable = true;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private string leftOrRight = "none";

    

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    bool canMove = true;
    Seeker seeker;
    public Rigidbody2D rb;



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
                rb.AddForce(Vector2.up * jumpSpeed * jumpModifier);
            }
        }

        if (attackZone.detectedCols.Count > 0)
        {
            canMove = false;
            activeDistance = 50;
        }
        else
        {
            canMove = true;
        }

        //Movement
        if (canMove)
        {
            rb.AddForce(force);
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
            if (leftOrRight == "left")
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
            else if (leftOrRight == "right") 
            {
                if (rb.velocity.x < 0.05f)
                {
                    transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
                }
                else if (rb.velocity.x > -0.05f)
                {
                    transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
                }
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
}
