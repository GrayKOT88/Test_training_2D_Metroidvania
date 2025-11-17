using UnityEngine;

[CreateAssetMenu (menuName = "Spells/Heal Spell")]
public class HealSpellSO : SpellSO
{
    [Header("Spark Settings")]
    public int healAmount = 10;    
    public GameObject healFXFXPrefab;
    

    public override void Cast(Player player)
    {
        GameObject newHealFX = Instantiate(healFXFXPrefab, player.transform.position, Quaternion.identity);
        Destroy(newHealFX, 2);

        player.health.ChangeHealth(healAmount);
    }
}