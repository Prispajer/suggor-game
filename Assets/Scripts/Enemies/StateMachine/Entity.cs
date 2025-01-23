using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    private GameObject HB { get;  set; }
    public AnimationToState ats { get; private set; }
    public int lastDamageDirection { get; private set; }
    public int facingDirection { get; private set; }

    public D_Entity entityData;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private HealthBar hb;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;



    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        aliveGO = transform.Find("Alive").gameObject;
        HB = aliveGO.transform.Find("HealthBar Canvas").gameObject;
        hb = HB.GetComponent<HealthBar>();
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        ats = aliveGO.GetComponent<AnimationToState>();
        stateMachine = new FiniteStateMachine();
        hb.SetMaxHealth(entityData.maxHealth);
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStun();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;

    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerMinRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerMaxRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerCloseRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRange, entityData.whatIsPlayer);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
        HB.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void ResetStun()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentStunResistance -= attackDetails.stunDamageAmount;
        currentHealth -= attackDetails.damageAmount;
        hb.SetHealth(currentHealth);
        DamageHop(entityData.damageHopSpeed);
        

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRange), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxDistance), 0.2f);
    }
}
