using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Character charBase;
    public StateMachine stateMachine;

    public State(StateMachine _stateMachine, Character charBase)
    {
        this.charBase = charBase;
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log("enter state: " + this.ToString());
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        Debug.Log("exit state: " + this.ToString());
    }
}
