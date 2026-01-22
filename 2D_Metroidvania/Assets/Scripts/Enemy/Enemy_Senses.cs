using UnityEngine;

public class Enemy_Senses : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    public bool IsAtCliff()
    {
        return !Physics2D.Raycast(groundCheck.position, Vector2.down, config.groundCheckDistance, config.groundLayer);
    }
    public bool IsHittingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.down, config.wallCheckDistance, config.wallLayer);
    }
}