using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class KnightSpecialAttackState : SpecialAttackState
    {
        public KnightSpecialAttackState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }

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
            _stateMachine.Target.GetDamaged(_stateMachine, 1f, 1);
            
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