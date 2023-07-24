using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class MoveState : State
{

    float speed = 5f;
    // Ÿ���� ���� �����ϴ� ����� ����.
    // Ÿ�ٰ� ��������� Ÿ���� �����ϴ� Attack State�� �����.
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
            // Player�� ���� ������.
            charBase.transform.LookAt(charBase.Target.transform);

            charBase.rigidbody.velocity = Caculation();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
