using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if(player.IsWallDetected() && ((xInput == player.facingDir) || xInput == 0) )
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if(xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y); // 由于翻转Sprite控制在设置移动速度里，这里不能直接设置rb的速度。

    }
}
