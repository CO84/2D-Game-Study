using UnityEngine;

public class PlayerFallState : PlayerAiredState
{
    public PlayerFallState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Update()
    {
        base.Update();

        //if player detects ground, transition to idle state
        if(player.isGroundDetected)
            stateMachine.ChangeState(player.idleState);

        if(player.isWallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
