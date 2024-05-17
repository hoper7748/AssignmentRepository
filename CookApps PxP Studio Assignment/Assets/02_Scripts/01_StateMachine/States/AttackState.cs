using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class AttackState : State
    {
        //private float _changeTimer = 0;
        private float _curTimer = 0;
        public AttackState(StateMachine _stateMachine) : base(_stateMachine)
        {
            _curTimer = 0;

        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log($"Attack / {_stateMachine.Transform.name}");
            _curTimer = 0;
            _stateMachine.Animator.SetTrigger("Attack");

            Vector3 direction  = _stateMachine.Transform.position - _stateMachine.Target.transform.position;
            _stateMachine.Transform.localScale = direction.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one;

            try
            {
                // 타겟에게 데미지 입히기
                _stateMachine.Target.GetDamaged(_stateMachine);

            }
            catch
            {
                Debug.Log("");
            }
           
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _curTimer += Time.deltaTime;
            if (_curTimer > 1f)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }

}