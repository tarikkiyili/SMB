public class PlayerWallClingState : PlayerState
{
    private int facingDirTemp;

    public PlayerWallClingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) {}

    public override void Enter()
    {
        base.Enter();
        facingDirTemp = player.facingDir;
        rb.gravityScale = 0;
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        rb.gravityScale = 2;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsWallDetected() || (xInput != 0 && xInput != facingDirTemp))
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        player.SetVelocity(0, yInput * player.climbingSpeed);

        if (player.IsGroundDetected() && !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
