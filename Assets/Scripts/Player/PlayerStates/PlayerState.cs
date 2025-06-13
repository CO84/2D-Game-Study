using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName)
    {
        this.player = player;

        animator = player.animator;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();
        
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        animator.SetFloat(GameConstans.YVelocity, rb.linearVelocity.y);
    }

    private bool CanDash()
    {
        if (player.isWallDetected)
            return false;

        if (stateMachine.CurrentState == player.dashState)
            return false;

        return true;
    }

}
