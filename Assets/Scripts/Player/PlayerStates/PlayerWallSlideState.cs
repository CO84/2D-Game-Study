using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);

        if(!player.isWallDetected)
            stateMachine.ChangeState(player.fallState);

        if(player.isGroundDetected)
        {
            stateMachine.ChangeState(player.idleState);

            if(player.facingDirection != player.moveInput.x)
                player.Flip(); // Ensure player is facing the correct direction when landing
        }
            
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);
    }
}
