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
    // 현재 서치 상태 주변에 적이 생성되면 해당 적을 향해 나아가는 MoveState로 변경됨.
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
            // 적일 경우 자동으로 Player를 세팅해줌.
            charBase.Target = Player.Instance.gameObject;
            bSearch = true;
        }
        else
        {
            // Player일 경우 원 범위를 체크하여 가장 가까운 적을 탐색 후 따라감.
        }
    }

    public override void Update()
    {
        base.Update();
        // 탐색 
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
