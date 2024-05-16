using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.Playable
{
    public class PPriest : Playable
    {
        private void Awake()
        {
            _maxHealthPoint = HealthPoint;
        }
        // Start is called before the first frame update
        void Start()
        {
            StateMachine = StateMachine.CreateStateMachine(this.gameObject);
            StateMachine.Initialize(StateMachine.IdleState);
            StateMachine.SetSpecialAttackState(this);
        }

        // Update is called once per frame
        void Update()
        {
            StateMachine.Update();
        }
    }
}