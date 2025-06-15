using UnityEngine;

public class EntityHealt : MonoBehaviour
{
    private Entity_VFX entityVFX;
    private Entity entity;

    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead = false;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 onDamageKnockback = new Vector2(1.5f, 2.5f);
    [SerializeField] private float knockbackDuration = 0.2f;

    [Header("On Heavy Damage Knockback")]
    [Range(0, 1)]
    [SerializeField] private float heavyDamageTreshold = .3f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    [SerializeField] private Vector2 onHeavyDamageKnockback = new Vector2(7f, 7f);

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
    }
    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        float duration = CalculateDuration(damage);
        Vector2 knockback = CalculateKnockback(damageDealer, damage);

        entityVFX?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockback(Transform damageDealer, float damage)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? onHeavyDamageKnockback : onDamageKnockback;
        knockback.x *= direction; // Apply direction based on position
        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }

    private bool IsHeavyDamage(float damage) => (damage / maxHealth) > heavyDamageTreshold;
}
