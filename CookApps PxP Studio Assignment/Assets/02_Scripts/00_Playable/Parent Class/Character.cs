using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        public LayerMask TargetLayer;

        [Header("UI"), Space(10f)]
        public Image EXPGageImage;
        public TextMeshProUGUI LvText;

        [HideInInspector] public float SearchRange = 10f;

        public Slider slider;
        public TextMeshProUGUI HPText;
        public TextMeshProUGUI AtkText;


        [HideInInspector] public float StunTime = 0;

        protected float MaxEXP = 100;
        protected float NowEXP = 0;

        protected int _lv = 1;

        public StateMachine StateMachine;

        public virtual void GetEXP(int EXP)
        {

        }

        public void Initialize(float _healthPoint, float _attackDamage, float _attackRange = 1f)
        {
            _maxHealthPoint = _healthPoint;
            HealthPoint = _healthPoint;
            AttackDamage = _attackDamage;
            AttackRange = _attackRange;
        }

        protected void LvUpdate()
        {
            if(LvText != null)
            {
                LvText.text = _lv.ToString();
            }
            else
            {
                Debug.Log("Not Have Lv");
            }
        }

        public void UpdateShopUI()
        {
            if(HPText != null && AtkText != null)
            {
                HPText.text = $"{HealthPoint} / {_maxHealthPoint}";
                AtkText.text = $"ATk {AttackDamage}";
            }
        }

        protected void EXPGageUpdate()
        {
            if(EXPGageImage != null)
            {
                EXPGageImage.fillAmount = NowEXP / MaxEXP;
            }
            else
            {
                Debug.Log("Not Have Image");
            }
        }

        public void GetDamaged(StateMachine _enemy, float _percent = 1f, float _stunTime = 0f)
        {
            if(HealthPoint <= 0)
            {
                return;
            }
            HealthPoint -= _enemy.Character.AttackDamage * _percent;
            StunTime = _stunTime;
            UpdateSlider();
            if (StunTime > 0 && HealthPoint > 0)
                StateMachine.ChangeState(StateMachine.StunState);
            else if (HealthPoint <= 0)
            {
                StateMachine.ChangeState(StateMachine.DeadState);
                _enemy.LostTarget();
            }
        }

        public void UpdateSlider()
        {
            slider.value = HealthPoint / _maxHealthPoint;
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