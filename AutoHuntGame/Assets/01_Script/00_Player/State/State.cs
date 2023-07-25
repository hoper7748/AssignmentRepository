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
        Debug.Log($"{charBase.gameObject.name} enter state: {this}");
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        Debug.Log($"{charBase.gameObject.name} exit state: {this}");
    }

    protected bool DeadCheck()
    {
        if (charBase.GetHP <= 0 && this.charBase.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            stateMachine.ChangeState(charBase.Dead);
            return true;
        }
        return false;
    }
}
