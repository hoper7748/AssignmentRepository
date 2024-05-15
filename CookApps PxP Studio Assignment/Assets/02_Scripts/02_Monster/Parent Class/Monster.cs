using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookAppsPxPAssignment.Character.Monster
{
    public class Monster : Character
    {
        public void SetHealthPoint()
        {
            // 체력 리셋.
            HealthPoint = _maxHealthPoint;
        }

    }

}