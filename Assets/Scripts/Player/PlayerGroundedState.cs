using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if (player.IsTouchingSaw())
        {
            stateMachine.ChangeState(player.deathState);
            return;
        }
            
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.climbState);

    }
}
