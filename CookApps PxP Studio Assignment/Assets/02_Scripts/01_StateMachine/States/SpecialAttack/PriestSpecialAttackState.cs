using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class PriestSpecialAttackState : SpecialAttackState
    {
        // 4미터 내의 아군을 회복시킴.
        public PriestSpecialAttackState(StateMachine _stateMachine) : base (_stateMachine)
        { 
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HealingOneTeamMember();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void HealingOneTeamMember()
        {
            Collider2D collider = Physics2D.OverlapCircle(_stateMachine.Transform.position, _stateMachine.Character.SkillRange, 1 << 3);
            if(collider != null)
                collider.GetComponent<Character>().Healling(_stateMachine.Character.AttackDamage * 2.5f);
        }
    }
}