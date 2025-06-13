using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float originalGravitiyScale;
    private int dashDirection;
    public PlayerDashState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection; 
        stateTimer = player.dashDuration;

        originalGravitiyScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * dashDirection, 0);

        if (stateTimer < 0)
        {
            if(player.isGroundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
            
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravitiyScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.isWallDetected)
        {
            if (player.isGroundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
