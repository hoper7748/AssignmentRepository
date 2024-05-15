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
        // ���� �ð����� ���� �������� ������ �̵��� ������.
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
            // �ð������ ���� Ž��
            _curTimer += Time.deltaTime;
            _attackCurTimer += Time.deltaTime;
            FindTarget();
            if (_stateMachine.Target != null) // Ÿ���� �����ϸ� �̵��ϴ� ���
            {
                BattleSequence();
                return;
            }

            // �߰����� �ƴ� ��.
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

            // ���� �Ǵ� �߰� ����
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
