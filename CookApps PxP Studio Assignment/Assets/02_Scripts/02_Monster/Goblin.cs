using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CookAppsPxPAssignment.Character.Monster
{
    public class Goblin : Monster
    {
        public Animator Animator;
        private void Awake()
        {
            _maxHealthPoint = HealthPoint;
            
        }
        // Start is called before the first frame update
        void Start()
        {
            StateMachine = StateMachine.CreateStateMachine(this.gameObject);
            StateMachine.Initialize(StateMachine.IdleState);
            StateMachine.Animator = Animator;
        }

        // Update is called once per frame
        void Update()
        {
            StateMachine.Update();
        }

        public void nEnable()
        {

        }
    }
}
