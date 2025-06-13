
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    #region Player States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    //public PlayerGroundedState groundedState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerBasicAttackState basicAttackState { get; private set; }
    public PlayerJumpAttackState jumpAttackState { get; private set; }

    #endregion

    #region Movement Details
    [Header("Movement Details")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    public Vector2 wallJumpForce;

    [Range(0, 1)] // Ensure this is between 0 and 1
    public float inAirMoveMultiplier = .7f; // shoul be from 0 to 1
    [Range(0, 1)] public float wallSlideSlowMultiplier = .7f;
    [Space] public float dashDuration = .25f;
    public float dashSpeed = 20;
   
    public Vector2 moveInput { get; private set; }

    #endregion

    #region Attack Details

    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1; // Time to reset combo index after an attack
    private Coroutine queuedAttackCor;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();

        idleState = new PlayerIdleState(StateMachine, GameConstans.PlayerIdleState, this);
        moveState = new PlayerMoveState(StateMachine, GameConstans.PlayerMoveState, this);
        jumpState = new PlayerJumpState(StateMachine, GameConstans.JumpFall, this);
        fallState = new PlayerFallState(StateMachine, GameConstans.JumpFall, this);
        wallSlideState = new PlayerWallSlideState(StateMachine, GameConstans.WallSlide, this);
        wallJumpState = new PlayerWallJumpState(StateMachine, GameConstans.JumpFall, this);
        dashState = new PlayerDashState(StateMachine, GameConstans.Dash, this);
        basicAttackState = new PlayerBasicAttackState(StateMachine, GameConstans.BasicAttack, this);
        jumpAttackState = new PlayerJumpAttackState(StateMachine, GameConstans.JumpAttack, this);
    }
    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(idleState);
    }
    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCor is not null)
            StopCoroutine(queuedAttackCor);

        queuedAttackCor = StartCoroutine(EnterAttackStateWithDelayCor());

    }
    // This coroutine allows the attack state to be entered after a delay
    // This is useful for ensuring the attack animation plays correctly
    // and the player has time to prepare for the next action.
    private IEnumerator EnterAttackStateWithDelayCor()
    {
        yield return new WaitForEndOfFrame();
        StateMachine.ChangeState(basicAttackState);
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }


    private void OnDisable()
    {
        input.Disable();
    }
}