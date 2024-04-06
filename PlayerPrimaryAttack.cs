using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;       // ��ϼ��ܱ��

    private float lastTimeAttacked; // �ϸ����ܽ���ʱ��
    private float comboWindow = 2f; // ��ϼ���ʱ�������
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
        // ���ù������̵�������΢�ƶ�
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y); 

        stateTimer = .1f; // �����ʱ�����ƶ�ֹͣ���빥��״̬��֤���ݵĹ����ƶ�
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);

        comboCounter++;
        lastTimeAttacked = Time.time; // ��¼��ǰʱ��
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