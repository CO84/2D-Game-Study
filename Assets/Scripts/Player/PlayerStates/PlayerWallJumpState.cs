using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDirection, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);

        if (player.isWallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
