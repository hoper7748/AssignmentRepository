using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : State
{
    
    private bool bSearch = false;
    private float waitTime = 1f;
    private float timer = 0;
    // ���� ��ġ ���� �ֺ��� ���� �����Ǹ� �ش� ���� ���� ���ư��� MoveState�� �����.
    public IdleState(StateMachine _stateMachine, Character _charBase) : base(_stateMachine, _charBase)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }


    private void Search()
    {
        if(charBase.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ���� ��� �ڵ����� Player�� ��������.
            charBase.Target = Player.Instance.gameObject;
            bSearch = true;
        }
        else
        {
            // Player�� ��� �� ������ üũ�Ͽ� ���� ����� ���� Ž�� �� ����.
        }
    }

    public override void Update()
    {
        base.Update();
        // Ž�� 
        if(timer < waitTime)
        {
            timer += Time.deltaTime;
            return;
        }
        
        Search();
        if (bSearch)
            stateMachine.ChangeState(charBase.Move);

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit IdleState");
    }
}
