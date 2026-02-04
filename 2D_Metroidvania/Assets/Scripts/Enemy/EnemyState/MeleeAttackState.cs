using UnityEngine;

public class MeleeAttackState : State
{
    protected override string AnimBoolName => "iaAttacking";
    public MeleeAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
    }
}