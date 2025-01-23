using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    public Transform GroundCheck;

    public LayerMask whatisground;

    public PhysicsMaterial2D player;
    public PhysicsMaterial2D playerFriction;


    public float movementSpeed;
    public float jumpForce;
    public float groundcheckradius;
   

    public int amountOfJumps = 1;


    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStats ps;
    


    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private float knockbackDuration;
    
    private bool knockback; 
    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canjump;
    private bool canFlip = true;
    private bool canMove = true;


    private float movementInputDirection;
    private float knockbackStartTime;

    private int jumpsLeft;
    private int facingDirection = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        checkJump();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        CheckSurroudings();
        ApplyMovement();
    }
    private void CheckSurroudings()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundcheckradius, whatisground);
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {      
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        if (movementInputDirection != 0 && Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else 
        {
            isWalking = false;
        }
        if (movementInputDirection != 0 )
        {
            rb.sharedMaterial = player;
        }
        else
        {
            rb.sharedMaterial = playerFriction;
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

      

    }
    private void checkJump()
    {
        if (isGrounded && rb.velocity.y < 0.01f)
        {
            jumpsLeft = amountOfJumps;
        }
        if (jumpsLeft <= 0)
        {
            canjump = false;
        }
        else
        {
            canjump = true;
        }
    }
 

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    private void Jump()
    {
        if(canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft--;
        }
 
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
    private void ApplyMovement()
    {
       
        if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void EnableFlip()
    {
        canFlip = true;
    }
    private void DisableFlip()
    {
        canFlip = false;

    }

    private void StopMove()
    {
        canFlip = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundcheckradius);
    }
}
