using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    private string animBoolName;

    protected float xInput;
    protected float yInput;

    protected float stateTimer;
    protected bool triggerCalled;

    protected Rigidbody2D rb;


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter() 
    {
        enemy.anim.SetBool(animBoolName, true);

        rb = enemy.rb;

        triggerCalled = false;
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
