using UnityEngine;

public class PlayerBasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private int attackDirection;
    private int comboIndex = 1; // Track the combo index for attacks
    private int comboLimit = 3;
    private float lastTimeAttacked; // Track the last time the player attacked
    private bool comboAttackQueued = false; // Flag to indicate if combo attack is queued
    public PlayerBasicAttackState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
        if(comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length; // Ensure combo limit matches the length of attack velocities
    }

    override public void Enter()
    {
        base.Enter();
        comboAttackQueued = false; 
        ResetComboIndexIfNeeded();

        attackDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection; 

        animator.SetInteger(GameConstans.ComboIndex, comboIndex);
        ApplyAttackVelocity();

    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if(input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack(); 

        if (triggerCalled)
            HandleStateExit();
        


    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            animator.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay(); // Queue the next attack
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++; // Increment the combo index for the next attack

        //remember time when we attack
        lastTimeAttacked = Time.time;
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;

    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1]; // Get the attack velocity based on the combo index

        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        //if time we attacked was long ago, reset combo index
        if (Time.time > (lastTimeAttacked + player.comboResetTime))
            comboIndex = GameConstans.FirstComboIndex; 

        if (comboIndex > comboLimit)
            comboIndex = GameConstans.FirstComboIndex; // Reset combo index if it exceeds the limit
    }
}
