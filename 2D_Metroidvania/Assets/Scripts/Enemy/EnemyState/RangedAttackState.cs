using UnityEngine;

public class RangedAttackState : State
{
    protected override string AnimBoolName => "isShooting";

    public RangedAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
    }

}
