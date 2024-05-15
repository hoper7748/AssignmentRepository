using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class IdleState : State
    {
        private float _curTimer;
        private float _attackCurTimer;
        private float _changeTimer;
        // 일정 시간동안 적이 존재하지 않으면 이동을 감행함.
        public IdleState(StateMachine _stateMachine) : base (_stateMachine)
        {
            _curTimer = 0;
            _attackCurTimer = 0;
            _changeTimer = 3f;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _curTimer = 0;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // 시간경과에 따라 탐색
            _curTimer += Time.deltaTime;
            _attackCurTimer += Time.deltaTime;
            FindTarget();
            if (_stateMachine.Target != null) // 타겟이 존재하면 이동하는 노드
            {
                BattleSequence();
                return;
            }

            // 추격중이 아닐 때.
            if (!_stateMachine.isChasing && _curTimer > _changeTimer)
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
                return;
            }
        }

        #region Magic Methods
        private void BattleSequence()
        {
            if (!_stateMachine.isChasing)
                _stateMachine.isChasing = true;
            float distance = Vector3.Distance(_stateMachine.Transform.position, _stateMachine.Target.transform.position);

            // 공격 또는 추격 진행
            if (distance <= _stateMachine.Character.AttackRange
                && _attackCurTimer > _stateMachine.Character.AttackCooldown)
            {
                _stateMachine.ChangeState(_stateMachine.AttackState);
                _attackCurTimer = 0;
            }
            else if (distance > _stateMachine.Character.AttackRange)
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
            }
        }

        private void FindTarget()
        {
            if (_stateMachine.Target == null)
                _stateMachine.SearchEnemy();
        }
        #endregion
    }
}
