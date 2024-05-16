using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.States
{
    public abstract class State
    {
        protected StateMachine _stateMachine;

        public State(StateMachine _stateMachine) 
        {
            this._stateMachine = _stateMachine;
        }

        public virtual void OnEnter()
        {
            //Debug.Log($"{_stateMachine.Transform.name} / Enter State = {this.GetType().Name}");
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }
    }

}