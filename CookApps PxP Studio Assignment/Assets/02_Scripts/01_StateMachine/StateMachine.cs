using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character
{
    public class StateMachine
    {
        public Transform Transform;
        public Character Character;
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        public States.State CurrentState;

        public States.IdleState IdleState;
        public States.MoveState MoveState;
        public States.AttackState AttackState;
        public States.DeadState DeadState;

        public Character Target;

        public Vector3[] Path;
        public int targetIndex;

        public bool isChasing = false;
        public bool Reversal = false;


        public static StateMachine CreateStateMachine(GameObject _playable)
        {
            StateMachine stateMachine = new StateMachine();

            stateMachine.Transform = _playable.transform;
            stateMachine.Character = _playable.GetComponent<Character>();
            stateMachine.Animator = _playable.GetComponent<Animator>();

            stateMachine.IdleState = new States.IdleState(stateMachine);
            stateMachine.MoveState = new States.MoveState(stateMachine);
            stateMachine.AttackState = new States.AttackState(stateMachine);
            stateMachine.DeadState = new States.DeadState(stateMachine);

            return stateMachine;
        }

        public void Initialize(States.State _startState)
        {
            CurrentState = _startState;
            CurrentState.OnEnter();
        }

        public void ChangeState(States.State _nextState)
        {
            CurrentState.OnExit();
            CurrentState = _nextState;
            CurrentState.OnEnter();
        }
        
        public void Update()
        {
            CurrentState.OnUpdate();
        }

        public void SearchEnemy()
        {
            // 임시 반지름 5f
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Transform.position, Character.SearchRange);
            Target = null;
            float oldDistance = 0;
            float newDistance = 0;
            // 가장 가까운 적을 향해 공격 이는 모든 플레이어가 같음.
            foreach (var col in colliders)
            {
                newDistance = Vector3.Distance(Transform.position, col.transform.position);
                if ((Target == null && col.transform != Transform) || (col.transform != Transform && newDistance < oldDistance))
                {
                    Target = col.transform.GetComponent<Character>();
                    Debug.Log($"{Target.name}");
                    oldDistance = newDistance;
                }
            }
        }

        public void SetAnimationTrigger(string _triggerName)
        {
            Animator.SetTrigger(_triggerName);
        }

        public void SetAnimationBoolean(string _booleanName, bool _value = true)
        {
            Animator.SetBool(_booleanName, _value);
        }

        // 적이 죽으면 호출하는 매서드
        public void LostTarget()
        {
            Target = null;
            isChasing = false;
        }

        public void GetPath(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                //chasing = true;
                targetIndex = 0;
                Path = newPath;
            }
        }
    }
}