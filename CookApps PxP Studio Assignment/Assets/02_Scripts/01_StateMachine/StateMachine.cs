using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character
{
    public enum ATTACKTYPE
    {
        NormalAttack,
        SpecialAttack,
    }

    public class StateMachine
    {
        public Transform Transform;
        public Character Character;
        public Animator Animator;
        public Collider2D Collider;
        public SpriteRenderer SpriteRenderer;
        public States.State CurrentState;

        public States.IdleState IdleState;
        public States.MoveState MoveState;
        public States.AttackState AttackState;
        public States.DeadState DeadState;
        public States.StunState StunState;
        public States.SpecialAttackState SpecialAttackState;

        public Character Target;

        public Vector3[] Path;
        public int targetIndex;

        public bool isChasing = false;
        public bool Reversal = false;
        public bool isDead = false;

        public ATTACKTYPE atkType = ATTACKTYPE.NormalAttack;


        public static StateMachine CreateStateMachine(GameObject _playable)
        {
            StateMachine stateMachine = new StateMachine();

            stateMachine.Transform = _playable.transform;
            stateMachine.Character = _playable.GetComponent<Character>();
            stateMachine.Animator = _playable.GetComponent<Animator>();
            stateMachine.Collider = _playable.GetComponent<Collider2D>();

            stateMachine.IdleState = new States.IdleState(stateMachine);
            stateMachine.MoveState = new States.MoveState(stateMachine);
            stateMachine.AttackState = new States.AttackState(stateMachine);
            stateMachine.StunState = new States.StunState(stateMachine);
            stateMachine.DeadState = new States.DeadState(stateMachine);


            return stateMachine;
        }

        public void SetSpecialAttackState(Playable.Playable charractor)
        {
            if (charractor is Playable.PArcher archer)
            {
                SpecialAttackState = new States.ArcherSpecialAttackState(this);
                return;
            }
            if (charractor is Playable.PKnight knight)
            {
                SpecialAttackState = new States.KnightSpecialAttackState(this);
                return;
            }
            if (charractor is Playable.PThrief thrief)
            {
                SpecialAttackState = new States.ThriefSpecialAttackState(this);
                return;
            }
            if (charractor is Playable.PPriest priest)
            {
                SpecialAttackState = new States.PriestSpecialAttackState(this);
                return;
            }
            if(Character is Monster.Monster monster)
            {
                SpecialAttackState = null;
                return;
            }
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

        public void SearchEnemy(float Range = 0f)
        {
            // 임시 반지름 5f
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Transform.position, Range == 0 ? Character.SearchRange : Range, Character.TargetLayer);
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
                    //Debug.Log($"{Target.name}");
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

        public void ResetHealthPoint()
        {
            Character.ResetHealth();
        }


        public void ChangeAttackType()
        {
            if(SpecialAttackState != null)
            {
                atkType = atkType == ATTACKTYPE.NormalAttack ? ATTACKTYPE.SpecialAttack : ATTACKTYPE.NormalAttack;
            }
        }
    }
}