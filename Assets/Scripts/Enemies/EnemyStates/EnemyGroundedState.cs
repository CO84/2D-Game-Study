using UnityEngine;

public class EnemyGroundedState : EnemyState
{
    public EnemyGroundedState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.battleState);
    }
}
