using UnityEngine;

public class EntityCombat : MonoBehaviour
{

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatisTarget;

    public float damage = 10;

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            EntityHealt targetHealth = target.GetComponent<EntityHealt>();

            targetHealth?.TakeDamage(damage, transform);
         }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatisTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
