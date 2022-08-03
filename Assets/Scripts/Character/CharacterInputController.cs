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
	public class CharacterInputController : MonoBehaviour
	{

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseInputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(mouseInputRay, out hit))
                {
                    Vector3 moveToPosition = hit.point;

                    foreach (var motor in FormationManager.Instance.currentSelectedUnits)
                    {
                        motor.MoveToTargetPosition(moveToPosition);
                    }
                }
            }

            //CharacterMotor minMotor = FormationManager.Instance.currentSelectedUnits.GetMin(d => Vector3.Distance(d.transform.position, d.navMeshAgent.destination));
            //Debug.Log("min dist: " + Vector3.Distance(minMotor.transform.position, minMotor.navMeshAgent.destination));
        }

       
    }
}