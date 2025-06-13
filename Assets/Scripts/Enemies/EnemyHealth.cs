using UnityEngine;

public class EnemyHealth : EntityHealt
{
    private Enemy enemy => GetComponent<Enemy>();
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if(damageDealer.CompareTag(GameConstans.PlayerTag)) //bu �ekilde de yap�labilir => if (damageDealer.GetComponent<Player>() is not null)
                enemy.TryToEnterBattleState(damageDealer);

        base.TakeDamage(damage, damageDealer);
    }
}
