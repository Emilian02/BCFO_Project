using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("VARIABLES")]
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float resetSpeed = 1.0f;
    [SerializeField] private float runSpeed = 1.0f;
    [SerializeField] private float slideSpeed = 1.0f;
    [SerializeField] private float crouchSpeed = 1.0f;
    [SerializeField] private float jumpForce = 600.0f;
    [SerializeField] private Animator animator;

    [Header("PLAYER Mechanics")]
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private string downInput;
    [SerializeField] private string shiftRun;
    [SerializeField] private string Attack1;
    [SerializeField] private string Attack2;
    [SerializeField] private string Attack3;
    [SerializeField] private string Dodge;

    [Header("DRAGABLE OBJECTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool canJump = false;
    private float horizontalMovement;

    [Header("HIT BOXES")]
    [SerializeField] public GameObject attackPoint1;
    [SerializeField] public GameObject attackPoint2;
    [SerializeField] public GameObject attackPoint3;
    [SerializeField] public GameObject forwardAttackPoint1;
    [SerializeField] public GameObject forwardAttackPoint3;
    [SerializeField] public GameObject runAttackPoint3;
    [SerializeField] public GameObject dahAttackPoint1;
    [SerializeField] public GameObject dahAttackPoint2;
    [SerializeField] public GameObject downAttackPoint1;
    [SerializeField] public GameObject downAttackPoint2;
    [SerializeField] public GameObject downAttackPoint3;
    [SerializeField] public GameObject launchAttackPoint3;
    [SerializeField] public float radius;
    [SerializeField] public float swordRadius;
    public LayerMask enemies;

    [Header("AUDIO")]
    public AudioClip electricAudio;
    AudioSource audioPlay;

    private bool slide = false;

    void Start()
    {
        audioPlay = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleMovement();
        HandleInputs();
    }

    private void HandleMovement()
    {

        if (Input.GetButtonDown(verticalInput) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
            animator.SetBool("HasJumped", true);
        }

        if (Input.GetButtonDown(downInput) || (Input.GetButton(downInput) && Input.GetButton(horizontalInput)))
        {
            moveSpeed = crouchSpeed;
            animator.SetBool("IsCrouching", true);
        }
        
        if (Input.GetButtonUp(downInput))
        {
            moveSpeed = resetSpeed;
            animator.SetBool("IsCrouching", false);
            animator.SetBool("IsRunning", false);
        }

        horizontalMovement = Input.GetAxisRaw(horizontalInput) * moveSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));

        if (Input.GetButton(horizontalInput) && Input.GetButtonDown(shiftRun))
        {
            moveSpeed = runSpeed;
            animator.SetBool("IsRunning", true);
            Debug.Log("running");
        }

        if (Input.GetButtonUp(shiftRun) || Input.GetButtonUp(horizontalInput))
        {
            moveSpeed = resetSpeed;
            animator.SetBool("IsRunning", false);
            //animator.SetBool("RunAttack3", false);
        }

        if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleInputs()
    {
        if (Input.GetButton(horizontalInput) && Input.GetButton(downInput))
        {
            Debug.Log("down-forward");
        }

        if (Input.GetButtonDown(downInput))
        {
            Debug.Log("down");
        }

        if (Input.GetButtonDown(verticalInput))
        {
            Debug.Log("Jump");
        }

        if (Input.GetButtonDown(horizontalInput))
        {
            Debug.Log("forward");
        }

        if (Input.GetButtonDown(Attack1) && (Input.GetButton(horizontalInput) && Input.GetButton(downInput)))
        {
            animator.SetBool("WHF", true);
            Debug.Log("WHF");
        }
        else if (Input.GetButtonDown(Attack1) && Input.GetButton(downInput))
        {
            slide = true;
            animator.SetBool("DownA2", true);
            if (spriteRenderer.flipX == false)
            {
                rb.AddForce(transform.right * slideSpeed, ForceMode2D.Impulse);
            }
            else if (spriteRenderer.flipX == true)
            {
                rb.AddForce(transform.right * -slideSpeed, ForceMode2D.Impulse);
            }
            Debug.Log("Slide");
        }
        else if (Input.GetButtonDown(Attack1))
        {
            animator.SetBool("Attack1", true);
            Debug.Log("attack1");
        }

        if(Input.GetButtonDown(Attack2) && Input.GetButton(downInput))
        {
            animator.SetBool("DownA1", true);
            Debug.Log("downA1");
        }
        else if(Input.GetButtonDown(Attack2) && Input.GetButton(horizontalInput))
        {
            animator.SetBool("ForwardA1", true);
            Debug.Log("forwardA1");
        }
        else if (Input.GetButtonDown(Attack2))
        {
            animator.SetBool("Attack2", true);
            Debug.Log("attack2");
        }

        if (Input.GetButtonDown(Attack3) && Input.GetButton(horizontalInput) && Input.GetButton(downInput))
        {
            animator.SetBool("LAUNCH", true);
            Debug.Log("LAUNCH");
        }
        else if (Input.GetButton(Attack3) && Input.GetButton(horizontalInput))
        {
            animator.SetBool("ForwardA3", true);
            Debug.Log("forwardAttack3");
        }
        else if (Input.GetButtonDown(Attack3) && Input.GetButton(downInput))
        {
            animator.SetBool("DownA3", true);
            Debug.Log("downA3");
        }
        else if (Input.GetButtonDown(Attack3))
        {
            animator.SetBool("Attack3", true);
            Debug.Log("attack3");
        }

        if (Input.GetButton(Attack3) && Input.GetButton(shiftRun))
        {
            animator.SetBool("RunAttack3", true);
            animator.SetBool("ForwardA3", false);
            Debug.Log("runAttack3");
        }
    }

    public void attack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void attack2()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint2.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void attack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint3.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void forwardAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(forwardAttackPoint3.transform.position, swordRadius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void forwardAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(forwardAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void downAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void downAttack2()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint2.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void downAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint3.transform.position, swordRadius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void runAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(runAttackPoint3.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void dahAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(dahAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("HitEnemy");
        }
    }
    public void dahAttack2()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(dahAttackPoint2.transform.position, radius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
           Debug.Log("HitEnemy");
       } 
    }
    public void launchAttack3()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(launchAttackPoint3.transform.position, swordRadius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
           Debug.Log("HitEnemy");
       } 
    }

    public void endAttack1()
    {
        animator.SetBool("Attack1", false);
    }
    
    public void endAttack2()
    {
        animator.SetBool("Attack2", false);
    }

    public void endAttack3()
    {
        animator.SetBool("Attack3", false);
    }
    
    public void endForwardAttack3()
    {
        animator.SetBool("ForwardA3", false);
    }
    public void endForwardAttack1()
    {
        animator.SetBool("ForwardA1", false);
    }
    public void endDownAttack2()
    {
        slide = false;
        animator.SetBool("DownA2", false);
    }
    public void endDownAttack1()
    {
        animator.SetBool("DownA1", false);
    }
    public void endDownAttack3()
    {
        animator.SetBool("DownA3", false);
    }
    public void endRunAttack3()
    {
        animator.SetBool("RunAttack3", false);
    }
    public void endDAHAttack2()
    {
        animator.SetBool("DAH", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("WHF", false);
    }
    public void endDAHAttack1()
    {
        animator.SetBool("WHF", false);
    }
    public void endLAUNCHAttack3()
    {
        animator.SetBool("LAUNCH", false);
        animator.SetBool("ForwardA3", false);
    }

    public void DAH()
    {
        Debug.Log("Can EWHF");
        if (Input.GetButton(Attack1))
        {
            Debug.Log("EWHF");
            animator.SetBool("DAH", true);
            audioPlay.PlayOneShot(electricAudio, 1.0f);
        }
        else
        {
            Debug.Log("DAHn't");
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint3.transform.position, radius);
        Gizmos.DrawWireSphere(forwardAttackPoint3.transform.position, radius);
        Gizmos.DrawWireSphere(forwardAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(runAttackPoint3.transform.position, radius);
        Gizmos.DrawWireSphere(dahAttackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(dahAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(downAttackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(downAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(launchAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(downAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(forwardAttackPoint3.transform.position, swordRadius);
    }

    private void FixedUpdate()
    {
        if (slide == false)
        {
            rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
        }
    }

    public void LandedOnPlatform()
    {
        canJump = true;
        animator.SetBool("HasJumped", false);
    }

    public void FellOffPlatform()
    {
        canJump = false;
        animator.SetBool("HasJumped", true);
    }
}