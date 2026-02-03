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
}