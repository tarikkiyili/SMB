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

        // else if (player.IsWallDetected() && Input.GetKeyDown(KeyCode.C))
        //     stateMachine.ChangeState(player.grabState);  
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }
}
