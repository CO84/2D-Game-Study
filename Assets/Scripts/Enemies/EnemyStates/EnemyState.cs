using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState( StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        rb = enemy.rb;
        animator = enemy.animator;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        animator.SetFloat(GameConstans.BattleAnimSpeedMultiplier, battleAnimSpeedMultiplier);
        animator.SetFloat(GameConstans.MoveAnimSpeedMultiplier, enemy.moveAnimSpeedMultiplier);
        animator.SetFloat(GameConstans.XVelocity, rb.linearVelocity.x);

    }
}
