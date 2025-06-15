using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private Collider2D collider2D;
    public Enemy_DeadState(StateMachine stateMachine, string animBoolName, Enemy enemy) : base(stateMachine, animBoolName, enemy)
    {
        collider2D = enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        animator.enabled = false;
        collider2D.enabled = false;
        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        stateMachine.SwitchOffStateMachine();
    }
}
