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
            // 죽으면 아무것도 모태

        }

        //private async UniTaskVoid UnitEnable()
        //{
        //    await UniTask.Delay(TimeSpan.FromSeconds(1f));
        //    // 1초 뒤 사라짐
        //    _stateMachine.Transform.gameObject.SetActive(false);
        //}
    }
}