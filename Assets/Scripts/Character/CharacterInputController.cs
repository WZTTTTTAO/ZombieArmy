using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 角色输入控制
	/// </summary>
	public class CharacterInputController : MonoSingleton<CharacterInputController>
	{
        public bool receiveInput { get; set; } = true;


        private void Update()
        {
            CameraInputControl();

            //CharacterMotor minMotor = FormationManager.Instance.currentSelectedUnits.GetMin(d => Vector3.Distance(d.transform.position, d.navMeshAgent.destination));
            //Debug.Log("min dist: " + Vector3.Distance(minMotor.transform.position, minMotor.navMeshAgent.destination));
        }

        private void CameraInputControl()
        {
            
        }

        public void MoveCurrentUnitsToMousePosition()
        {
            Ray mouseInputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseInputRay, out hit))
            {
                Vector3 moveToPosition = hit.point;

                foreach (var motor in FormationManager.Instance.currentSelectedUnits)
                {
                    motor.MoveToTargetPosition(moveToPosition);
                }
            }
        }

    }
}