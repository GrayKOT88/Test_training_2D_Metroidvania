using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float horizontalJumpPerfcent = 0.5f;
    public PlayerWallJumpState(Player player) : base(player) { }

    public override void Enter()
    {
        anim.SetBool("isWallJumping", true);

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = new Vector2(-player.facingDirection * horizontalJumpPerfcent, 1f) * player.jumpForce;

        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        if (JumpPressed && player.isTouchingWall)
            player.ChangeState(player.wallJumpState);

        else if (player.isGrounded && rb.linearVelocity.y <= 0.1f)
            player.ChangeState(player.idleState);
    }

    public override void FixedUpdate()
    {
        player.ApplyVariableGravity();

        if(JumpPressed && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * player.jumpCutMultiplier);
            JumpReleased = false;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isWallJumping", false);
    }
}