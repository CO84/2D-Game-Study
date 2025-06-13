using UnityEngine;

public class EnemeySkeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(StateMachine, GameConstans.EnemyIdleState, this);
        moveState = new Enemy_MoveState(StateMachine, GameConstans.EnemyMoveState, this);
        attackState = new Enemy_AttackState(StateMachine, GameConstans.EnemyAttackState, this);
        battleState = new Enemy_BattleState(StateMachine, GameConstans.EnemyBattleState, this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState);
    }
}
