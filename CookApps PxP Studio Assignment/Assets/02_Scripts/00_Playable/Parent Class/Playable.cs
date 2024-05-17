using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.Playable
{
    public class Playable : Character
    {
        protected Cinemachine.CinemachineBrain brainCamera;
        public Cinemachine.CinemachineVirtualCamera virCamera;


        public void ChangeCamera()
        {
            virCamera.MoveToTopOfPrioritySubqueue();
            Debug.Log("Camera Change");

        }

        public override void GetEXP(int EXP)
        {
            base.GetEXP(EXP);
            // 죽은 상태에서는 경험치를 얻지 못하게 함.  
            if (StateMachine.isDead)
                return;
            // 게이지를 채움.
            NowEXP += EXP;
            if(NowEXP >= 100)
            {
                _lv++;
                NowEXP = 0;
                LvUpdate();
            }
            EXPGageUpdate();
        }

        public void AddAttackDamage(float _value)
        { 
            if(Manager.GameManager.Instance.UseGold(5))
                AttackDamage += _value;
            UpdateShopUI();
            //Manager.GameManager.Instance.

        }
        public void AddMaxHealthPoint( float _value)
        {
            if(Manager.GameManager.Instance.UseGold(10))
                _maxHealthPoint += _value;
            UpdateShopUI();
            UpdateSlider();


        }
    }

}