using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public Health health;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
    }

    private void HandleDamage()
    {
        anim.SetTrigger("isDamaged");
    }
}