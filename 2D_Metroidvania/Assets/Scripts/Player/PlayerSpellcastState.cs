using UnityEngine;

public class PlayerSpellcastState : PlayerState
{
    public PlayerSpellcastState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isCasting", true);
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();

        if (Mathf.Abs(MoveInput.x) > 0.1f)
            player.ChangeState(player.moveState);
        else
            player.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isCasting", false);
    }
}