using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private float knockbackVelocity;
    private float knockbackDuretion;
    private bool isTimeSlow;

    public PlayerDeathState(Player player) : base(player) { }

    public void SetParameters(int knockbackDirection)
    {
        knockbackVelocity = knockbackDirection * damage.knockbackForce;
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0.3f;
        isTimeSlow = true;
        anim.SetBool("isDead", true);

        //player.groundCheckRadius = 0.2f;
        knockbackDuretion = damage.knockbackDuration;
        player.rb.linearVelocity = new Vector2(knockbackVelocity, player.rb.linearVelocity.y);
    }

    public override void FixedUpdate()
    {
        knockbackDuretion -= Time.fixedDeltaTime;

        if (knockbackDuretion <= 0)
        {
            if (isTimeSlow)
            {
                Time.timeScale = 1f;
                isTimeSlow = false;
            }
            if(player.isGrounded)
                player.rb.linearVelocity = Vector2.zero;            
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isdead", false);
        //player.groundCheckRadius = 0.35f;
    }
}