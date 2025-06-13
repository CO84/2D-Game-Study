using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeInBattle;

    public Enemy_BattleState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    override public void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        player ??= enemy.GetPlayerReference();

        if (SholdRetreat())
        {
            rb.linearVelocity = new Vector2(-enemy.retreatVelocity.x * DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected())
            UpdateBattleTimer();

        if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if (WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.attackState);
        //if player detected bu far away move to player
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    private void UpdateBattleTimer() => lastTimeInBattle = Time.time;
    private bool BattleTimeIsOver() => Time.time > lastTimeInBattle + enemy.battleTimeDuration;
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;
    private bool SholdRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;
    private float DistanceToPlayer()
    {

        if (player is null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);

    }

    private int DirectionToPlayer()
    {
        if (player is null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
