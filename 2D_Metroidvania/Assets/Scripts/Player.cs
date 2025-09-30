using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public Animator anim;

    [Header("Movement Variables")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float jumpCutMultiplier = 0.5f;
    public float normalGraviry;
    public float fallGravity;
    public float jumpGravity;

    public int facingDirection = 1;

    public Vector2 moveInput;
    private bool runPressed;
    private bool jumpPressed;
    private bool jumpReleased;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;

    private void Start()
    {
        rb.gravityScale = normalGraviry;
    }

    private void Update()
    {
        Flip();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        ApplyVariableGravity();
        CheckGrounded();
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float currentSpeed = runPressed ? runSpeed : walkSpeed;
        float targetSpeed = moveInput.x * currentSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if(jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
            jumpReleased = false;
        }
        if (jumpReleased)
        {
            if(rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
            jumpReleased = false;
        }
    }

    private void ApplyVariableGravity()
    {
        if(rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravity;
        }
        else if(rb.linearVelocity.y > 0.1f)
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = normalGraviry;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandleAnimations()
    {
        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f);
        anim.SetBool("isGrounded", isGrounded);

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(moveInput.x) > 0.1 && isGrounded;

        anim.SetBool("isIdle", !isMoving && isGrounded);
        anim.SetBool("isWalk", isMoving && !runPressed);
        anim.SetBool("isRunning", isMoving && runPressed);
    }

    private void Flip()
    {
        if(moveInput.x > 0.1f)
        {
            facingDirection = 1;
        }
        else if(moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    public void OnMove (InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRun (InputValue value)
    {
        runPressed = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            jumpPressed = true;
            jumpReleased = false;
        }
        else
        {
            jumpReleased = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
