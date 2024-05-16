using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;


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
            if (_stateMachine.Character is Playable.Playable)
            {
                _stateMachine.Collider.enabled = false;
                UnitResurrection().Forget();
                _stateMachine.isDead = true;
                Manager.GameManager.Instance.DeadCheck();
            }
            if (_stateMachine.Character is Monster.Monster )
            {
                //Debug.Log("Hello world");
                Manager.SpawnManager.Instance.curSpawnAmount--;
                GameObject.Destroy(_stateMachine.Transform.gameObject, 1f);
            }
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
        
        private async UniTaskVoid UnitResurrection()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_stateMachine.Character.SpawnTimer));
            _stateMachine.SetAnimationTrigger("Alive");
            Manager.GameManager.Instance.AliveCheck();
            _stateMachine.ResetHealthPoint();
            _stateMachine.isDead = false;
            _stateMachine.Collider.enabled = true;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        //private async UniTaskVoid UnitEnable()
        //{
        //    await UniTask.Delay(TimeSpan.FromSeconds(1f));
        //    // 1초 뒤 사라짐
        //    _stateMachine.Transform.gameObject.
        //}
    }
}