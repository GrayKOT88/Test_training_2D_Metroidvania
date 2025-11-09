using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;
    public SpellSO currentSpell;
    public bool CanCast => Time.time >= nextCastTime;
    private float nextCastTime;

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    private void CastSpell()
    {
        if (!CanCast || currentSpell == null)
            return;

        currentSpell.Cast(player);
        nextCastTime = Time.time + currentSpell.cooldown;        
    }
}