using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Cysharp.Threading.Tasks;
//using System;

namespace CookAppsPxPAssignment.Character
{
    public class Character : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            // ¼­Ä¡ ¹üÀ§ °â Å½»ö ¹üÀ§
            Gizmos.DrawWireSphere(transform.position, SearchRange);

            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        public float SpawnTimer = 5f;

        protected float _maxHealthPoint = 0;
        public float HealthPoint = 400;
        public float AttackDamage = 5;

        public float AttackRange = 1f;
        public float AttackCooldown = 1.5f;

        public float SkillRange = 1f;
        public float SkillCoolDown = 3f;

        public float SearchRange = 10f;

        [HideInInspector] public float StunTime = 0;


        public StateMachine StateMachine;

        public Slider slider;

        public LayerMask TargetLayer;

        public void GetDamaged(StateMachine _enemy, float _percent = 1f, float _stunTime = 0f)
        {
            if(HealthPoint <= 0)
            {
                return;
            }
            HealthPoint -= _enemy.Character.AttackDamage * _percent;
            StunTime = _stunTime;
            slider.value = HealthPoint / _maxHealthPoint;

            if (StunTime > 0 && HealthPoint > 0)
                StateMachine.ChangeState(StateMachine.StunState);
            else if (HealthPoint <= 0)
            {
                StateMachine.ChangeState(StateMachine.DeadState);
                _enemy.LostTarget();

            }
        }

        public void Healling(float _healValue)
        {
            HealthPoint += _healValue;
            if (_maxHealthPoint < HealthPoint)
                HealthPoint = _maxHealthPoint;

            slider.value = HealthPoint / _maxHealthPoint;

        }

        public void ResetHealth()
        {
            HealthPoint = _maxHealthPoint;
            slider.value = HealthPoint / _maxHealthPoint;
        }
    }

}