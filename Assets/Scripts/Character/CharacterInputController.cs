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

                //如果左键点击僵尸，则让僵尸进行移动
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                {
                    UnitManager.Instance.ChangeSelectedUnits(hit.transform.parent);
                }
                //如果没有点击到僵尸 则可以进行屏幕拖拽
                else 
                {
                    CameraController.Instance.OnLeftMousePressed();
                }
                
            }

            else if (Input.GetMouseButtonDown(1))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (!Physics.Raycast(mouseRay, out hit)) return;

                //如果点击到地面视为移动
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    UnitManager.Instance.unitsCanAttack = false;
                    UnitManager.Instance.MoveCurrentUnitsToMousePosition();
                }
                //如果点击到敌人视为A过去
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
                {
                    UnitManager.Instance.unitsCanAttack = true;
                    UnitManager.Instance.MoveCurrentUnitsToMousePosition();
                }
                else
                    UnitManager.Instance.unitsCanAttack = false;


            }


        }
    }
}