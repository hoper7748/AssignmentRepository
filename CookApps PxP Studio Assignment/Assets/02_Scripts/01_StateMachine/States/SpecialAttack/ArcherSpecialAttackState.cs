using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{

    public class ArcherSpecialAttackState : SpecialAttackState
    {
        // 4m���� �� ���� ��󿡰� ���ݷ� 250% ��ŭ�� �������� ��.
        public ArcherSpecialAttackState(StateMachine _stateMachine) : base(_stateMachine) { }
        public override void OnEnter()
        {
            base.OnEnter();
            if (_stateMachine.Target == null)
                _stateMachine.SearchEnemy(_stateMachine.Character.SkillRange);
            // ��ã�ѳ�?
            if(_stateMachine.Target == null)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
            _stateMachine.Target.GetDamaged(_stateMachine, 2.5f);
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}