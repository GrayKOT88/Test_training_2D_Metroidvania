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
    public bool CanRangedAttack() => Time.time > lasrAttackTime + config.rangedCooldown;

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

    public void PerformRangedAttack()
    {
        lasrAttackTime = Time.time;

        Vector2 fireDirection = (enemy.CurrentTarget.position - attackPoint.position).normalized;
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Deg2Rad;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject newProjectile = Instantiate(config.projectilePrefab, attackPoint.position, rotation);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.Damage = config.rangedDamage;
        projectile.Lifetime = config.projectileLifetime;

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = fireDirection * config.projectileSpeed;
    }
}
