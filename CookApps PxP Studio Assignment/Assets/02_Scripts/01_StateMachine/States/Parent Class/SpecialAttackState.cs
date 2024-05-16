using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CookAppsPxPAssignment.Character.States
{
    public class SpecialAttackState : AttackState
    {
        float _curTimer;
        public SpecialAttackState(StateMachine _stateMachine) : base(_stateMachine)
        {

        }

        public override void OnEnter()
        {
            //base.OnEnter();
            _stateMachine.SetAnimationTrigger("Special");
            //Debug.Log("Using SpecialAttack");
            Debug.Log($"SpecialAttack / {_stateMachine.Transform.name}");
            _curTimer = 0;
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