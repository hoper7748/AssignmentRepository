using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StateMachine
{
    public State CurrentState;
    //public Character CharBase;

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(State newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
