using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class ThriefSpecialAttackState : SpecialAttackState
    {
        // 주변 2M 모든 적들에게 피해를 입힘.
        public ThriefSpecialAttackState(StateMachine _stateMachine) : base (_stateMachine)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            // 범위 탐색
            SearchMonsters();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        
        private void SearchMonsters()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_stateMachine.Transform.position, _stateMachine.Character.SkillRange, _stateMachine.Character.TargetLayer);
            
            // 모든 적을 찾으면 데미지를 준다.
            foreach(var collider in colliders)
            {
                collider.GetComponent<Character>().GetDamaged(_stateMachine);
            }
        }
    }

}