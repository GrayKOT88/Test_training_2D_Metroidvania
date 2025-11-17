using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Teleport Spell")]
public class TeleportSpellSO : SpellSO
{
    [Header("Teleport Settings")]
    public float range = 5;
    public float playerRadius = 1.5f;    
    public LayerMask obstacleLayer;

    public override void Cast(Player player)
    {
        Vector2 direction = new Vector2(player.facingDirection, 0);
        Vector2 targetPosition = (Vector2)player.transform.position + direction * range;

        Collider2D hit = Physics2D.OverlapCircle(targetPosition, playerRadius, obstacleLayer);

        if (hit != null)
        {
            float step = 0.1f;
            Vector2 adjustedPosition = targetPosition;

            while (hit != null && Vector2.Distance(adjustedPosition, player.transform.position) > 0)
            {
                adjustedPosition -= direction * step;
                hit = Physics2D.OverlapCircle(adjustedPosition, playerRadius, obstacleLayer);
            }
            targetPosition = adjustedPosition;
        }

        player.transform.position = targetPosition;
    }
}