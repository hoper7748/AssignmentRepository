using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public class StunState : State
    {
        float _curTimer = 0;
        float _stunTime = 0;
        public StunState(StateMachine _stateMachine) : base (_stateMachine)
        { 
            
        }
        public override void OnEnter()
        {
            base.OnEnter();
            _curTimer = 0;
            _stunTime = _stateMachine.Character.StunTime;
            Debug.Log("기절");
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _curTimer += Time.deltaTime;
            if (_curTimer > _stunTime)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }
}
