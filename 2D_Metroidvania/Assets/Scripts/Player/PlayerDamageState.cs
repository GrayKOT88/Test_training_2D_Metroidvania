using UnityEngine;

public class PlayerDamageState : PlayerState
{
    private float timer;
    private float knockbackVelocity;
    private float knockbackDuretion;
    public PlayerDamageState(Player player) : base(player) { }

    public void SetParameters(int knockbackDirection)
    {
        knockbackVelocity = knockbackDirection * damage.knockbackForce;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isDamaged", true);

        knockbackDuretion = damage.knockbackDuration;
        player.rb.linearVelocity = new Vector2(knockbackVelocity, player.rb.linearVelocity.y);
    }

    public override void FixedUpdate()
    {
        knockbackDuretion -= Time.fixedDeltaTime;
        if (knockbackDuretion <= 0)
        {
            player.rb.linearVelocity = Vector2.zero;
            player.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isDamaged", false);
    }
}