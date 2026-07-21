using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("General")]
    public float turnThreshold = 0.2f;

    [Header("Patrol")]
    public float patrolSpeed = 5;
    public float groundCheckDistance = 0.7f;
    public float wallCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Chase")]
    public float chaseSpeed = 7;
    public float chaseRange = 5;
    public LayerMask targetLayer;

    [Header("Attack")]
    public float meleeRange = 1.2f;
    public int meleeDamage = 2;
    public float meleeCooldown = 1;

    [Header("Ranged Attack")]
    public float rangedRange = 5;
    public int rangedDamage = 1;
    public float rangedCooldown = 2;
    public GameObject projectilePrefab;
    public float projectileSpeed = 12;
    public float projectileLifetime = 3;

    [Header("Damaged")]
    public float knockbackDuration = 0.2f;
    public float knockbackForce = 30;
}