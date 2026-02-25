using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    private EnemyConfig config;
    private Enemy enemy;
    private float lasrAttackTime;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        config = enemy.Config;
    }

    public bool CanMeleeAttack() => Time.time > lasrAttackTime + config.meleeCooldown;

    public void PerformMeleeAttack()
    {
        lasrAttackTime = Time.time;

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, config.meleeRange, config.targetLayer);
        if (!hit)
            return;
        Health health = hit.GetComponentInChildren<Health>();
        if (health != null)
            health.ChangeHealth(-config.meleeDamage, transform.position);
    }
}
