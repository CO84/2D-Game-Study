using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    [Header("Battle Details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity;

    [Header("Movement Details")]
    public float moveSpeed = 1.4f;
    public float idleTime = 2f;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1f;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;

    public Transform player { get; private set; }

    public override void EntityDeath()
    {
        base.EntityDeath();
        StateMachine.ChangeState(deadState);
    }

    public void TryToEnterBattleState(Transform player)
    {
        if (StateMachine.CurrentState == battleState) return;
        if (StateMachine.CurrentState == attackState) return;

        this.player = player;
        StateMachine.ChangeState(battleState);
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, playerCheckDistance,  whatIsGround | whatIsPlayer);

        if(hit.collider is null || hit.collider.gameObject.layer != LayerMask.NameToLayer(GameConstans.Player))
            return default;

        return hit;
    }

    public Transform GetPlayerReference()
    {
        if(player is null)
            player = PlayerDetected().transform;


        return player;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + Vector3.right * playerCheckDistance * facingDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + Vector3.right * attackDistance * facingDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + Vector3.right * minRetreatDistance * facingDirection);
    }
}
