using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class ThriefSpecialAttackState : SpecialAttackState
    {
        // �ֺ� 2M ��� ���鿡�� ���ظ� ����.
        public ThriefSpecialAttackState(StateMachine _stateMachine) : base (_stateMachine)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            // ���� Ž��
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
            
            // ��� ���� ã���� �������� �ش�.
            foreach(var collider in colliders)
            {
                collider.GetComponent<Character>().GetDamaged(_stateMachine);
            }
        }
    }

}