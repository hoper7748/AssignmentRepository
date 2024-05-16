using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class IdleState : State
    {
        //private float _curTimer;
        //private float _attackCurTimer;
        private float _skillCoolDown;
        private float _attackCoolDown;
        //private float _changeTimer;

        // 일정 시간동안 적이 존재하지 않으면 이동을 감행함.
        public IdleState(StateMachine _stateMachine) : base (_stateMachine)
        {
            //_curTimer = 0;
            //_attackCurTimer = 0;
            //_changeTimer = 3f;
            _skillCoolDown = _stateMachine.Character.SkillCoolDown;
            _attackCoolDown = _stateMachine.Character.AttackCooldown;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //_curTimer = 0;
        }

        public void Enter()
        {

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // 시간경과에 따라 탐색
            _attackCoolDown -= Time.deltaTime;
            _skillCoolDown -= Time.deltaTime;
            
            FindTarget();
            if (_stateMachine.Target != null)
                BattleSequence1();
            else
            {
                if (_stateMachine.isChasing)
                    _stateMachine.isChasing = false;
                _stateMachine.ChangeState(_stateMachine.MoveState);
                return;
            }


            //_curTimer += Time.deltaTime;
            //FindTarget();
            //if (_stateMachine.Target != null) // 타겟이 존재하면 이동하는 노드
            //{
            //    BattleSequence();
            //    return;
            //}

            //// 추격중이 아닐 때.
            //if (!_stateMachine.isChasing && _curTimer > _changeTimer)
            //{
            //    _stateMachine.ChangeState(_stateMachine.MoveState);
            //    return;
            //}
        }

        #region Magic Methods

        private void BattleSequence1()
        {
            if (!_stateMachine.isChasing)
                _stateMachine.isChasing = true;

            // 타이머 체크 0 미만이면 쿨이 찬것.
            if(_stateMachine.SpecialAttackState != null && _skillCoolDown < 0)
            {
                SpecialAttack();
            }
            else if(_attackCoolDown < 0)
            {
                NormalAttack();
            }

        }

        private void BattleSequence()
        {
            if (!_stateMachine.isChasing)
                _stateMachine.isChasing = true;
            
            switch (_stateMachine.atkType)
            {
                case ATTACKTYPE.NormalAttack:
                    NormalAttack();
                    break;
                case ATTACKTYPE.SpecialAttack:
                    SpecialAttack();
                    break;
                default:
                    break;
            }

        }

        private void NormalAttack()
        {
            float distance = Vector3.Distance(_stateMachine.Transform.position, _stateMachine.Target.transform.position);
            // 공격 또는 추격 진행
            if (distance <= _stateMachine.Character.AttackRange)
            {
                _stateMachine.ChangeState(_stateMachine.AttackState);
                _stateMachine.ChangeAttackType();
                _attackCoolDown = _stateMachine.Character.AttackCooldown;
            }
            else if (distance > _stateMachine.Character.AttackRange)
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
            }
        }
        private void SpecialAttack()
        {
            float distance = Vector3.Distance(_stateMachine.Transform.position, _stateMachine.Target.transform.position);

            if (distance <= _stateMachine.Character.SkillRange)
            {
                _stateMachine.ChangeState(_stateMachine.SpecialAttackState);
                _stateMachine.ChangeAttackType();
                _skillCoolDown = _stateMachine.Character.SkillCoolDown;
                _attackCoolDown = _stateMachine.Character.AttackCooldown;
            }
            else if (distance > _stateMachine.Character.SkillRange)
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
            }

        }

        private void FindTarget()
        {
            if (_stateMachine.Target != null && _stateMachine.Target.GetComponent<Character>().StateMachine.isDead)
                _stateMachine.Target = null;
            if (_stateMachine.Target == null)
                _stateMachine.SearchEnemy();
        }
        #endregion
    }
}
