using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(StateMachine _stateMachine, Character charBase) : base(_stateMachine, charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
        charBase.rigidbody.velocity = Vector3.zero;
        // ���� ��� �ð��� ���Ҵٸ� �������� ����.
        if (!charBase.isAttack())
            return;
        charBase.animator.SetBool("isWalk", false);
        charBase.animator.SetTrigger("isAttack");
        charBase.ResetAtkTimer();
    }

    public override void Update()       
    {
        base.Update();
        if(!charBase.isAttack())
            stateMachine.ChangeState(charBase.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
