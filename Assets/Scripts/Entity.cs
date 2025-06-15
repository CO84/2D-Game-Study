using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    protected StateMachine StateMachine;

    private bool isFacingRight = true;
    public int facingDirection { get; private set; } = 1; // 1 for right, -1 for left


    #region Collision Detection

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    [SerializeField] private Transform groundCheck;
    public bool isGroundDetected { get; private set; }
    public bool isWallDetected { get; private set; }
    #endregion


    //Knockback variables
    private Coroutine knockbackCoroutine;
    private bool isKnockedBack = false;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine = new StateMachine();

    }


    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        HandleCollisionDetection();
        StateMachine.UpdateActiveState();
    }

    public virtual void EntityDeath()
    {

    }

    public void ReciveKnockback(Vector2 knockback, float duration)
    {
        if (knockbackCoroutine is not null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackCoroutine(duration, knockback));
    }

    private IEnumerator KnockbackCoroutine(float duration, Vector2 knockback)
    { 
        isKnockedBack = true;
        rb.linearVelocity = knockback;

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero; // Reset velocity after knockback
        isKnockedBack = false;
    }

    public void CurrentStateAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if(isKnockedBack)
            return; // Prevent setting velocity if knocked back

        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !isFacingRight)
            Flip();
        else if (xVelocity < 0 && isFacingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        facingDirection *= -1; // Toggle facing direction
    }

    private void HandleCollisionDetection()
    {
        isGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        isWallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if (secondaryWallCheck is not null)
        {
            isWallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround)
                     && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        }

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.right * (wallCheckDistance * facingDirection));   
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));

        if (secondaryWallCheck is not null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }
}
