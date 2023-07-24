using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public DeadState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
