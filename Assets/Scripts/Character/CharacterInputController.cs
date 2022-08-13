using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 角色输入控制器
	/// </summary>
	public class CharacterInputController : MonoBehaviour
	{
        private RaycastHit hit;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(mouseRay, out hit)) return;

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                {
                    UnitManager.Instance.ChangeSelectedUnits(hit.transform.parent);
                }
                else 
                {
                    CameraController.Instance.OnLeftMousePressed();
                }
                
            }

            else if (Input.GetMouseButtonDown(1))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (!Physics.Raycast(mouseRay, out hit)) return;

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    UnitManager.Instance.unitsCanAttack = false;
                    UnitManager.Instance.MoveCurrentUnitsToMousePosition();
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
                {
                    UnitManager.Instance.unitsCanAttack = true;
                }
                else
                    UnitManager.Instance.unitsCanAttack = false;


            }


        }
    }
}