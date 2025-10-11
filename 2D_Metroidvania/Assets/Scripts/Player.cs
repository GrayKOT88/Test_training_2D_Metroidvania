using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public Animator anim;
    public CapsuleCollider2D playerCollider;

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

    [Header("Ground Check")]
    public Transform headCheck;
    public float headCheckRadius = 0.2f;
    
    [Header("Slide Settings")]
    public float slideDuration = 0.6f;
    public float slideSpeed = 12;
    public float slideStopDuration = 0.15f;

    public float slideHeight;
    public Vector2 slideOffset;
    public float normalHeight;
    public Vector2 normalOffset;

    private bool isSliding;
    private bool slideInputLocked;
    private float slideTimer;
    private float slideStopTimer;

    private void Start()
    {
        rb.gravityScale = normalGraviry;
    }

    private void Update()
    {
        TryStandUp();

        if(!isSliding)
            Flip();

        HandleAnimations();
        HandleSlide();
    }

    private void FixedUpdate()
    {
        ApplyVariableGravity();
        CheckGrounded();

        if(!isSliding)
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

    private void HandleSlide()
    {
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(slideSpeed * facingDirection, rb.linearVelocity.y);

            if(slideTimer <= 0)
            {
                isSliding = false;
                slideStopTimer = slideStopDuration;
                TryStandUp();
            }
        }

        if(slideStopTimer > 0)
        {
            slideStopTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if(isGrounded && runPressed && moveInput.y < - 0.1f && !isSliding && !slideInputLocked)
        {
            isSliding = true;
            slideInputLocked = true;
            slideTimer = slideDuration;
            SetColliderSlide();
        }

        if(slideStopTimer < 0 && moveInput.y >= -0.1f)
        {
            slideInputLocked = false;
        }
    }

    private void TryStandUp()
    {
        if (isSliding)
        {
            anim.SetBool("isCrouching", false);
            return;
        }
        
        bool shouldCrouch = moveInput.y <= - 0.1f || Physics2D.OverlapCircle(headCheck.position, headCheckRadius, groundLayer);

        if (!shouldCrouch)
        {
            SetColliderNormal();
            anim.SetBool("isCrouching", false);
        }
        else
        {
            SetColliderSlide();
            anim.SetBool("isCrouching", true);
        }
    }

    private void SetColliderNormal()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalHeight);
        playerCollider.offset = normalOffset;
    }

    private void SetColliderSlide()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, slideHeight);
        playerCollider.offset = slideOffset;
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
        bool isCrouching = anim.GetBool("isCrouching");

        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isSliding", isSliding);

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(moveInput.x) > 0.1 && isGrounded;

        anim.SetBool("isIdle", !isMoving && isGrounded && !isSliding && !isCrouching);
        anim.SetBool("isWalk", isMoving && !runPressed && !isSliding && !isCrouching);
        anim.SetBool("isRunning", isMoving && runPressed && !isSliding && !isCrouching);
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);
    }
}
