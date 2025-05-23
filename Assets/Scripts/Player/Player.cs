using UnityEngine;

public class Player : Entity
{
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
    }

    [Header("Move info")]
    public float jumpForce;

    [Header("Climbing Info")]
    public float climbingSpeed = 15f;

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallClingState climbState { get; private set; }
    public PlayerDeathState deathState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        climbState = new PlayerWallClingState(this, stateMachine, "Climb");
        deathState = new PlayerDeathState(this, stateMachine, "Death");
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();   
    }

    public bool IsTouchingSaw()
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Saw"));
        contactFilter.useLayerMask = true;

        Collider2D[] results = new Collider2D[5];
        int count = myCollider.Overlap(contactFilter, results);

        return count > 0;
    }

}