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

        public float SearchRange = 10f;
        public StateMachine StateMachine;

        public Slider slider;

        public LayerMask TargetLayer;

        public void GetDamaged(StateMachine Enemy)
        {
            HealthPoint -= Enemy.Character.AttackDamage;
            slider.value = HealthPoint / _maxHealthPoint;
            if(HealthPoint <= 0)
            {
                StateMachine.ChangeState(StateMachine.DeadState);
                Enemy.LostTarget();
            }
        }
    }

}