using UnityEngine;

public class PlayerJumpAttackState : PlayerState
{
    private bool isTouchedGround;
    public PlayerJumpAttackState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isTouchedGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
 

    }

    public override void Update()
    {
        base.Update();

        if (player.isGroundDetected && isTouchedGround is false)
        {
            isTouchedGround = true;
            animator.SetTrigger(GameConstans.JumpAttackTrigger);
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && player.isGroundDetected)
            stateMachine.ChangeState(player.idleState);
    }
}
