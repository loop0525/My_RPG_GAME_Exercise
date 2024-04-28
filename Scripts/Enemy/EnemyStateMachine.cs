using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; private set; }
    
    public void Initialize(EnemyState _startState)
    {
        this.currentState = _startState;
        this.currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        this.currentState.Exit();
        this.currentState = _newState;
        this.currentState.Enter();
    }
}
