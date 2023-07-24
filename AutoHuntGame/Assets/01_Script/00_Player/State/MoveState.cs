using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class MoveState : State
{

    float speed = 5f;
    // 타겟을 향해 전진하는 기능을 가짐.
    // 타겟과 가까워지면 타겟을 공격하는 Attack State로 변경됨.
    public MoveState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
        charBase.animator.SetBool("isWalk", true);
    }

    private Vector3 Caculation()
    {
        Vector3 directionToMovement = charBase.Target.transform.position - charBase.transform.position;
        return directionToMovement.normalized * speed;
    }

    public override void Update()
    {
        base.Update();
        if(charBase.CheckAttackDistance())
        {
            stateMachine.ChangeState(charBase.Attack);
        }
        else
        {
            // Player를 향해 움직임.
            charBase.transform.LookAt(charBase.Target.transform);

            charBase.rigidbody.velocity = Caculation();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
