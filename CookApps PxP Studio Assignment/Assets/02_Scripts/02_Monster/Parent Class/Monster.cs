using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.Monster
{
    public class Monster : Character
    {
        public void SetHealthPoint()
        {
            // ü�� ����.
            HealthPoint = _maxHealthPoint;
            slider.value = HealthPoint / _maxHealthPoint;
        }

        public override void GetEXP(int EXP)
        {
            //base.GetEXP(EXP);
            Debug.Log("Monsters can't gain experience");
        }
    }

}