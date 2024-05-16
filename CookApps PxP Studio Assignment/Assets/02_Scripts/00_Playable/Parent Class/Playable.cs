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

    }

}