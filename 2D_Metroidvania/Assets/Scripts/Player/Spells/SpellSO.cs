using UnityEngine;

public abstract class SpellSO : CollectibleSO
{
    [Header("General")]    
    public float cooldown;

    public override void Collect(Player player)
    {
        player.magic.LearnSpell(this);
    }

    public abstract void Cast(Player player);
}
