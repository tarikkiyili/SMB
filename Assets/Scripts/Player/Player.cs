using UnityEngine;

public class Player : Entity
{
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        // Gizmos.DrawLine(ceilingCheck.position, new Vector3(ceilingCheck.position.x, ceilingCheck.position.y + ceilingCheckDistance));
    }

    [Header("Move info")]
    public float jumpForce;

    [Header("Climbing Info")]
    public float climbingSpeed = 15f;

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallClingState climbState {get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        climbState = new PlayerWallClingState(this, stateMachine, "Climb");
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
    // public override void Die()
    // {
    //     base.Die();
    //     stateMachine.ChangeState(deadState);
    //     GameManager.Instance.GameOver();
    // }
}