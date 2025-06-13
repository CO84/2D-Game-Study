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

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
    }
    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        Vector2 knockback = CalculateKnockback(damageDealer);

        entityVFX?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, knockbackDuration);
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
    }

    private Vector2 CalculateKnockback(Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = onDamageKnockback;
        knockback.x *= direction; // Apply direction based on position
        return knockback;
    }
}
