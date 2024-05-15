using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cysharp.Threading.Tasks;
//using System;


namespace CookAppsPxPAssignment.Character.States
{
    public class DeadState : State
    {
        public DeadState(StateMachine _stateMachine) : base(_stateMachine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _stateMachine.SetAnimationTrigger("Dead");
            //UnitEnable().Forget();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // ������ �ƹ��͵� ����

        }

        //private async UniTaskVoid UnitEnable()
        //{
        //    await UniTask.Delay(TimeSpan.FromSeconds(1f));
        //    // 1�� �� �����
        //    _stateMachine.Transform.gameObject.SetActive(false);
        //}
    }
}