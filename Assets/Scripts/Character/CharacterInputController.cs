using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 角色输入控制
	/// </summary>
	public class CharacterInputController : MonoBehaviour
	{
        public CharacterMotor[] allCharacterMotors;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseInputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(mouseInputRay, out hit))
                {
                    foreach (var motor in allCharacterMotors)
                    {
                        motor.MoveToTargetPosition(hit.point);
                    }
                }
            }
        }
    }
}