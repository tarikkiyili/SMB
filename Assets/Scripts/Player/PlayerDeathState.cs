public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
