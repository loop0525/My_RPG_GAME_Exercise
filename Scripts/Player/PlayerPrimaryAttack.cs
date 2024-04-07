using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter = 0;       // 组合技能编号

    private float lastTimeAttacked; // 上个技能结束时间
    private float comboWindow = 2f; // 组合技能时间最大间隔
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;
        if(xInput != 0)
            attackDir = xInput;

        // 设置攻击过程的任务轻微移动
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y); 

        stateTimer = .1f; // 这个定时用于移动停止进入攻击状态保证短暂的惯性移动
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", 0.15f);

        comboCounter++;
        lastTimeAttacked = Time.time; // 记录当前时间
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0 )
            player.ZeroVelocity();

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
