using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement")]
    public float patrolSpeed = 5;

    [Header("Patrol")]
    public float groundCheckDistance = 0.7f;
    public float wallCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
}