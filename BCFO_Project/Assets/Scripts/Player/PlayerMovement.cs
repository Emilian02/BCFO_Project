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
    [SerializeField] private float rollSpeed = 1.0f;
    [SerializeField] private float crouchSpeed = 1.0f;
    [SerializeField] private float jumpForce = 600.0f;
    [SerializeField] private Animator animator;

    [Header("PLAYER CONTROLS")]
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private string downInput;
    [SerializeField] private string shiftRun;
    [SerializeField] private string Attack1;
    [SerializeField] private string Attack2;
    [SerializeField] private string Attack3;
    [SerializeField] private string Dodge;
    [SerializeField] private string TeleportActivate;

    [Header("DRAGABLE OBJECTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool canJump = false;
    private float horizontalMovement;

    [Header("HIT BOXES")]
    [SerializeField] private GameObject attackPoint1;
    [SerializeField] private GameObject attackPoint2;
    [SerializeField] private GameObject attackPoint3;
    [SerializeField] private GameObject forwardAttackPoint1;
    [SerializeField] private GameObject forwardAttackPoint3;
    [SerializeField] private GameObject runAttackPoint3;
    [SerializeField] private GameObject dahAttackPoint1;
    [SerializeField] private GameObject dahAttackPoint2;
    [SerializeField] private GameObject downAttackPoint1;
    [SerializeField] private GameObject downAttackPoint2;
    [SerializeField] private GameObject downAttackPoint3;
    [SerializeField] private GameObject launchAttackPoint3;
    [SerializeField] private GameObject jumpAttackPoint1;
    [SerializeField] private GameObject jumpAttackPoint2;
    [SerializeField] private GameObject jumpAttackPoint3;
    [SerializeField] public float radius;
    [SerializeField] public float swordRadius;
    public LayerMask enemies;

    [Header("AUDIO")]
    public AudioClip electricAudio;
    public AudioClip stage0Audio;
    AudioSource audioPlay;
    AudioSource musicPlay;

    public static bool slide = false;
    private bool canAttack = true;
    private bool canAirAttack = true;
    private bool isCrouching = false;
    private bool isRunning = false;
    public static bool canTeleport = false;
    public static bool isBossFight = false;

    void Start()
    {
        audioPlay = GetComponent<AudioSource>();
        musicPlay = GetComponent<AudioSource>();
        musicPlay.PlayOneShot(stage0Audio, 0.1f);
        Physics2D.IgnoreLayerCollision(7, 3);
    }
    void StopMusic()
    {
        musicPlay.Stop();
    }
    void Update()
    {
        if (!startCutscene.isCutsceneOn)
        {
            HandleMovement();
            HandleInputs();
        }
        else if (startCutscene.isCutsceneOn || isBossFight == true)
        {
            StopMusic();
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsCrouching", false);
        }
    }

    private void HandleMovement()
    {

        if (Input.GetButtonDown(verticalInput) && canJump && canAttack && isCrouching == false)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
            animator.SetBool("HasJumped", true);
        }

        if ((Input.GetButtonDown(downInput) || (Input.GetButton(downInput) && Input.GetButton(horizontalInput))) && canJump == true)
        {
            moveSpeed = crouchSpeed;
            isCrouching = true;
            isRunning = false;
            animator.SetBool("IsCrouching", true);
        }
        
        if (Input.GetButtonUp(downInput))
        {
            moveSpeed = resetSpeed;
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            animator.SetBool("IsRunning", false);
        } 
        
        if (Input.GetButtonDown(TeleportActivate))
        {
            canTeleport = true;
            Debug.Log("Try to TP");
        }
        if (Input.GetButtonUp(TeleportActivate))
        {
            canTeleport = false;
        }

        if (canAttack == true)
        {
            horizontalMovement = Input.GetAxisRaw(horizontalInput) * moveSpeed;
            animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        }
        else if (canAttack == false)
        {
            horizontalMovement = Input.GetAxisRaw(horizontalInput) * 0;
            animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        }

        if (Input.GetButton(horizontalInput) && Input.GetButtonDown(shiftRun) && canJump == true)
        {
            moveSpeed = runSpeed;
            isRunning = true;
            animator.SetBool("IsRunning", true);
            Debug.Log("running");
        }

        if (Input.GetButton(horizontalInput) && Input.GetButtonDown(Dodge) && canJump == true && canAttack == true)
        {
            slide = true;
            animator.SetBool("DODGE", true);
            Debug.Log("DODGE");
            canAttack = false;
            rb.AddForce(transform.right * rollSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp(shiftRun) || Input.GetButtonUp(horizontalInput))
        {
            moveSpeed = resetSpeed;
            animator.SetBool("IsRunning", false);
            isRunning = false;
        }
        if (horizontalMovement < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (horizontalMovement > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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

        if (Input.GetButtonDown(Attack3) && canJump == false && canAirAttack == true)
        {
            canAirAttack = false;
            animator.SetBool("JumpA3", true);
        }
        if (Input.GetButtonDown(Attack1) && canJump == false && canAirAttack == true)
        {
            canAirAttack = false;
            animator.SetBool("JumpA2", true);
        }
        if (Input.GetButtonDown(Attack2) && canJump == false && canAirAttack == true)
        {
            canAirAttack = false;
            animator.SetBool("JumpA1", true);
        }

        if (canJump == true)
        {
            animator.SetBool("JumpA1", false);
            animator.SetBool("JumpA2", false);
            animator.SetBool("JumpA3", false);
            canAirAttack = true;
        }

        if (Input.GetButtonDown(Attack1) && (Input.GetButton(horizontalInput) && Input.GetButton(downInput)) && canAttack == true && canJump == true)
        {
            animator.SetBool("DAH", true);
            Debug.Log("DAH");
            audioPlay.PlayOneShot(electricAudio, 0.2f);
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack1) && (Input.GetButton(horizontalInput) && Input.GetButton(downInput)) && canAttack == true && canJump == true)
        {
            animator.SetBool("WHF", true);
            Debug.Log("WHF");
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack1) && Input.GetButton(downInput) && canAttack == true && canJump == true)
        {
            slide = true;
            animator.SetBool("DownA2", true);
            canAttack = false;
            rb.AddForce(transform.right * slideSpeed, ForceMode2D.Impulse);
            Debug.Log("Slide");
        }
        else if (Input.GetButtonDown(Attack1) && canAttack == true && canJump == true)
        {
            animator.SetBool("Attack1", true);
            Debug.Log("attack1");
            canAttack = false;
        }

        if (Input.GetButtonDown(Attack2) && Input.GetButton(downInput) && canAttack == true && canJump == true)
        {
            animator.SetBool("DownA1", true);
            Debug.Log("downA1");
            canAttack = false;
        }
        else if(Input.GetButtonDown(Attack2) && Input.GetButton(horizontalInput) && canAttack == true && canJump == true)
        {
            animator.SetBool("ForwardA1", true);
            Debug.Log("forwardA1");
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack2) && canAttack == true && canJump == true)
        {
            animator.SetBool("Attack2", true);
            Debug.Log("attack2");
            canAttack = false;
        }

        if (Input.GetButtonDown(Attack3) && Input.GetButton(shiftRun) && canAttack == true && canJump == true && isRunning == true)
        {
            slide = true;
            animator.SetBool("RunAttack3", true);
            animator.SetBool("ForwardA3", false);
            Debug.Log("runAttack3");
            canAttack = false;
        }

        if (Input.GetButtonDown(Attack3) && Input.GetButton(horizontalInput) && Input.GetButton(downInput) && canAttack == true && canJump == true)
        {
            animator.SetBool("LAUNCH", true);
            Debug.Log("LAUNCH");
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack3) && Input.GetButton(horizontalInput) && canAttack == true && canJump == true)
        {
            animator.SetBool("ForwardA3", true);
            Debug.Log("forwardAttack3");
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack3) && Input.GetButton(downInput) && canAttack == true && canJump == true)
        {
            animator.SetBool("DownA3", true);
            Debug.Log("downA3");
            canAttack = false;
        }
        else if (Input.GetButtonDown(Attack3) && canAttack == true && canJump == true)
        {
            animator.SetBool("Attack3", true);
            Debug.Log("attack3");
            canAttack = false;
        }
    }

    public void attack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 4;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 4f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void attack2()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint2.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 3;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 3f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void attack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint3.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 5;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 5f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void forwardAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(forwardAttackPoint3.transform.position, swordRadius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 5;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 6f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void forwardAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(forwardAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 4;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 4f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void downAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 3;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 3f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void downAttack2()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint2.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 2;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 7f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void downAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(downAttackPoint3.transform.position, swordRadius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 6;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 6f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void runAttack3()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(runAttackPoint3.transform.position, swordRadius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 3;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 7f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void dahAttack1()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(dahAttackPoint1.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 4;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 2f, ForceMode2D.Impulse);
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        }
    }
    public void dahAttack2()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(dahAttackPoint2.transform.position, swordRadius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 7;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 0.1f, ForceMode2D.Impulse);
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        } 
    }
    public void launchAttack3()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(launchAttackPoint3.transform.position, swordRadius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 3;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 2f, ForceMode2D.Impulse);
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        } 
    }
    public void jumpAttack3()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(jumpAttackPoint3.transform.position, radius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 4;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 5f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        } 
    }
    public void jumpAttack2()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(jumpAttackPoint2.transform.position, radius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 3;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 4f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        } 
    }
    public void jumpAttack1()
    {
       Collider2D[] enemy = Physics2D.OverlapCircleAll(jumpAttackPoint1.transform.position, swordRadius, enemies);

       foreach (Collider2D enemyGameobject in enemy)
       {
            enemyGameobject.GetComponent<EnemyHealth>().health -= 2;
            if (EnemyHealth.isBoss == false)
            {
                enemyGameobject.GetComponent<Rigidbody2D>().AddForce(transform.right * 3f, ForceMode2D.Impulse);
                if (enemyGameobject.tag == "Enemy1")
                {
                    enemyGameobject.GetComponent<EnemyAi1>().timer = 0.0f;
                    enemyGameobject.GetComponent<EnemyAi1>().hurt = true;
                }
            }
        } 
    }

    public void endAttack1()
    {
        animator.SetBool("Attack1", false);
        canAttack = true;
    }
    public void endAttack2()
    {
        animator.SetBool("Attack2", false);
        canAttack = true;
    }
    public void endAttack3()
    {
        animator.SetBool("Attack3", false);
        canAttack = true;
    }
    public void endForwardAttack3()
    {
        animator.SetBool("ForwardA3", false);
        canAttack = true;
    }
    public void endForwardAttack1()
    {
        animator.SetBool("ForwardA1", false);
        canAttack = true;
    }
    public void endDownAttack2()
    {
        slide = false;
        animator.SetBool("DownA2", false);
        canAttack = true;
    }
    public void endDownAttack1()
    {
        animator.SetBool("DownA1", false);
        canAttack = true;
    }
    public void endDownAttack3()
    {
        animator.SetBool("DownA3", false);
        canAttack = true;
    }
    public void endRunAttack3()
    {
        slide = false;
        animator.SetBool("RunAttack3", false);
        canAttack = true;
    }
    public void endDAHAttack2()
    {
        animator.SetBool("DAH", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("WHF", false);
        canAttack = true;
    }
    public void endDAHAttack1()
    {
        animator.SetBool("WHF", false);
        canAttack = true;
    }
    public void endLAUNCHAttack3()
    {
        animator.SetBool("LAUNCH", false);
        animator.SetBool("ForwardA3", false);
        canAttack = true;
    }
    public void endjumpAttack1()
    {
        animator.SetBool("JumpA1", false);
        canAirAttack = true;
    }
    public void endjumpAttack2()
    {
        animator.SetBool("JumpA2", false);
        canAirAttack = true;
    }
    public void endjumpAttack3()
    {
        animator.SetBool("JumpA3", false);
        canAirAttack = true;
    }
    public void endDodge()
    {
        animator.SetBool("DODGE", false);
        slide = false;
        canAttack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint3.transform.position, radius);
        Gizmos.DrawWireSphere(forwardAttackPoint3.transform.position, radius);
        Gizmos.DrawWireSphere(forwardAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(runAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(dahAttackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(dahAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(downAttackPoint2.transform.position, swordRadius);
        Gizmos.DrawWireSphere(downAttackPoint1.transform.position, radius);
        Gizmos.DrawWireSphere(launchAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(downAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(forwardAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(jumpAttackPoint3.transform.position, swordRadius);
        Gizmos.DrawWireSphere(jumpAttackPoint2.transform.position, radius);
        Gizmos.DrawWireSphere(jumpAttackPoint1.transform.position, radius);
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