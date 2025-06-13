using UnityEngine;

public class PlayerJumpState : PlayerAiredState
{
    public PlayerJumpState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //we need to sure we are not in the jump attack state when we are falling
        if (rb.linearVelocity.y < 0 && stateMachine.CurrentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);
    }
}
