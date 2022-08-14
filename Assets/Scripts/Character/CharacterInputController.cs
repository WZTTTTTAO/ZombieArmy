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

        [SerializeField] private Transform aimer;

        [SerializeField] private RectTransform screenDragArea;

        private RaycastHit[] raycastHits = new RaycastHit[5];

        private Ray mouseRay;

        private void Update()
        {
            aimer.gameObject.SetActive(RectTransformUtility.RectangleContainsScreenPoint(screenDragArea, Input.mousePosition));

            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(mouseRay, out hit)) return;

            SetAimerPosAndColor(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (!Physics.Raycast(mouseRay, out hit)) return;

                //如果左键点击僵尸，则让僵尸进行移动
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                {
                    UnitManager.Instance.ChangeSelectedUnits(hit.transform.parent);
                }
                //如果没有点击到僵尸 则可以进行屏幕拖拽
                else
                {
                    //CameraController.Instance.OnLeftMousePressed();
                }

            }

            else if (Input.GetMouseButtonDown(1))
            {
                //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (!Physics.Raycast(mouseRay, out hit)) return;

                //如果点击到地面视为移动
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    UnitManager.Instance.CurrentUnitsCanAttack = false;
                    UnitManager.Instance.MoveCurrentUnitsToMousePosition();
                }
                //如果点击到敌人视为A过去
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
                {
                    UnitManager.Instance.CurrentUnitsCanAttack = true;
                    UnitManager.Instance.MoveCurrentUnitsToMousePosition();
                }
                else
                    UnitManager.Instance.CurrentUnitsCanAttack = false;


            }


        }

        private RaycastHit GetRaycastHitByPriority()
        {
            bool hasZombie = false;
            RaycastHit zombieHit = raycastHits[0], groundHit = raycastHits[0];

            foreach (var raycastHit in raycastHits)
            {
                if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
                {
                    return raycastHit;
                }
                else if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
                {
                    zombieHit = raycastHit;
                    hit = raycastHit;
                    hasZombie = true;
                }
                else
                    groundHit = hit;
            }

            if (hasZombie)
                return zombieHit;
            else
                return groundHit;

            
        }

        private void SetAimerPosAndColor(Vector3 hitPos)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                aimer.position = new Vector3(hitPos.x, hitPos.y + 0.1f, hitPos.z);
                aimer.GetComponent<SpriteRenderer>().color = Color.blue;
                aimer.rotation = Quaternion.Euler(90, 0, 0);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
            {
                aimer.position = new Vector3(hitPos.x, hitPos.y + 0.1f, hitPos.z);
                aimer.GetComponent<SpriteRenderer>().color = Color.red;
                aimer.rotation = Quaternion.Euler(90, 0, 0);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                aimer.GetComponent<SpriteRenderer>().color = Color.green;
                aimer.position = new Vector3(hitPos.x, hitPos.y + 0.1f, hitPos.z);
                aimer.rotation = Quaternion.FromToRotation(transform.forward, hit.normal) * transform.rotation;
            }


        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(mouseRay);
        }
    }
}