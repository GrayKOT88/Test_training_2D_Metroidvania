using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Player player;
    public Health health;

    [Header("Knockback Settings")]
    public float knockbackForce = 20;
    public float knockbackDuration = 0.2f;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
        health.OnDeath -= HandleDeath;
    }

    private void HandleDamage(Vector2 sourcePoition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePoition.x ? 1 : -1;

        player.damageState.SetParameters(knockbackDir);
        player.ChangeState(player.damageState);
    }

    private void HandleDeath(Vector2 sourcePoition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePoition.x ? 1 : -1;

        player.deathState.SetParameters(knockbackDir);
        player.ChangeState(player.deathState);
    }
}