using UnityEngine;

public class PatrolState : State
{
    public PatrolState(Enemy enemy) : base(enemy) { }

    public override void FixedUpdate()
    {
        if(senses.IsHittingWall() || senses.IsAtCliff())
        {
            enemy.Flip();
            return;
        }
           

        rb.linearVelocity = new Vector2(config.patrolSpeed * enemy.FacingDirection, rb.linearVelocity.y);
    }
}
