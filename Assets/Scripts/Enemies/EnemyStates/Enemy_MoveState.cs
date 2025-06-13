using UnityEngine;

public class Enemy_MoveState : EnemyGroundedState
{
    public Enemy_MoveState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (!enemy.isGroundDetected || enemy.isWallDetected)
            enemy.Flip();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.linearVelocity.y);

        if (!enemy.isGroundDetected || enemy.isWallDetected)
            stateMachine.ChangeState(enemy.idleState);


    }
}
