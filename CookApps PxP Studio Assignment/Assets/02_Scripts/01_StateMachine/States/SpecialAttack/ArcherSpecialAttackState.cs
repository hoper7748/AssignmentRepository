using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{

    public class ArcherSpecialAttackState : SpecialAttackState
    {
        // 4m범위 내 단일 대상에게 공격력 250% 만큼의 데미지를 줌.
        public ArcherSpecialAttackState(StateMachine _stateMachine) : base(_stateMachine) { }
        public override void OnEnter()
        {
            base.OnEnter();
            if (_stateMachine.Target == null)
                _stateMachine.SearchEnemy(_stateMachine.Character.SkillRange);
            // 못찾앗나?
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