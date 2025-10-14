using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    public PlayerCrouchState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("isCrouching", true);
        player.SetColliderSlide();
    }

    public override void Update()
    {
        base.Update();

        if (JumpPressed)
        {
            //JumpPressed = false;
            ConsumeJumpInput();
            player.ChangeState(player.jumpState);
        }
        else if(MoveInput.y > - 0.1 && !player.CheckForCeiling())
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(Mathf.Abs(MoveInput.x) > 0.1f)
        {
            rb.linearVelocity = new Vector2(player.facingDirection * player.walkSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isCrouching", false);
        player.SetColliderNormal();
    }
}