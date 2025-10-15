using UnityEngine;
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isJumping", true);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);

        JumpPressed = false;
        JumpReleased = false;              
    }

    public override void Update()
    {
        base.Update();

        if (player.isGrounded && rb.linearVelocity.y <= 0)            
            player.ChangeState(player.idleState);         
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.ApplyVariableGravity();

        if(JumpReleased && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * player.jumpCutMultiplier);
            JumpReleased = false;
        }

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        float targetSpeed = speed * MoveInput.x;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isJumping", false);                
    }
}