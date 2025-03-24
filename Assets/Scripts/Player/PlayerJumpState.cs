using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float jumpStartTime;
    private float groundCheckDisableTime = 0.3f;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
        jumpStartTime = Time.time;
    }
    public override void Update()
    {
        base.Update();
        if (Time.time >= jumpStartTime + groundCheckDisableTime && player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.climbState);
            
        player.SetVelocity(player.moveSpeed * .8f * xInput, rb.linearVelocity.y);

        

    }
    public override void Exit()
    {
        base.Exit();
    }
}
