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
            //Debug.Log($"Enter State = {this.ToString()}");
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }
    }

}