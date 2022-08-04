using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 角色马达：角色移动、旋转
	/// </summary>
	public class CharacterMotor : MonoBehaviour
	{
		//角色移动的目标点位
		//[SerializeField] private Transform targetDestinationTrans;

        [SerializeField] private float rotateSpeed;

		private NavMeshAgent navMeshAgent;
        //上一次旋转协程
        //private Coroutine lastRotationCoroutine;
        //是否完成寻路
        private bool pathCompleted = true;
        //被选中icon
        private GameObject selectedIconGO;

        private void Start()
        {
			navMeshAgent = GetComponent<NavMeshAgent>();
            selectedIconGO = transform.Find("Selected").gameObject;
        }

        /// <summary>
        /// 注册单位到达目的地事件
        /// </summary>
        public void RegisterArriveEvent()
        {
            EventManager.AddListener("UnitArriveDestination", OnUnitArriveDestination);
        }

        /// <summary>
        /// 开启关闭选中图标
        /// </summary>
        /// <param name="visible">是否显示图标</param>
        public void SetSelectedIconVisibility(bool visible)
        {
            selectedIconGO.SetActive(visible);
        }

        /// <summary>
        /// 注销单位到达目的地事件
        /// </summary>
        public void UnregisterArriveEvent()
        {
            EventManager.RemoveListener("UnitArriveDestination", OnUnitArriveDestination);
        }

        //任意一个单位到达终点后 其他单位停止移动
        private void OnUnitArriveDestination()
        {
            StopMove();
        }

        private void Update()
        {
            //只有开始移动后（寻路未完成） 才进行是否到达目的地检测
            if (pathCompleted) return;

            //到达目的地后 触发到达目的地事件
            if (Vector3.Distance(transform.position, navMeshAgent.destination) < navMeshAgent.stoppingDistance)
            {
                EventManager.TriggerEvent("UnitArriveDestination");
                pathCompleted = true;
            }
        }

        /// <summary>
        /// 角色寻路到目标点位置
        /// </summary>
        /// <param name="targetPosition">目标位置坐标</param>
        public void MoveToTargetPosition(Vector3 targetPosition)
        {
            //RotateTowardTarget(targetPosition);

            navMeshAgent.SetDestination(targetPosition);

            pathCompleted = false;
        }

        public void SetNavAgentStopped(bool stop)
        {
            navMeshAgent.isStopped = stop;
        }

        public void StopMove()
        {
            navMeshAgent.ResetPath();
            pathCompleted = true;
        }

        #region 旋转
        //private Quaternion CalculateTargetRotation(Vector3 targetPos)
        //{
        //    //旋转方向向量
        //    Vector3 lookDirection = targetPos - transform.position;
        //    //目标旋转角度
        //    return Quaternion.LookRotation(lookDirection);
        //}

        /// <summary>
        /// 旋转朝向目标点
        /// </summary>
        /// <param name="targetPos">面向的目标点</param>
        //public void RotateTowardTarget(Vector3 targetPos)
        //{
        //    //如果目标点和当前位置重合则不需要选择
        //    if ((targetPos - transform.position).sqrMagnitude < 0.1f) return;
        //    //计算目标旋转
        //    Quaternion targetRotation = CalculateTargetRotation(targetPos);
        //    //逐渐旋转到目标旋转角度
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //}

        //public void GraduallyRotateTowardTarget(Vector3 targetPos)
        //{
        //    //如果目标点和当前位置重合则不需要选择
        //    if ((targetPos - transform.position).sqrMagnitude < 0.1f) return;
        //    //计算目标旋转
        //    Quaternion targetRotation = CalculateTargetRotation(targetPos);
        //    //停止上一次旋转
        //    if (lastRotationCoroutine != null)
        //        StopCoroutine(lastRotationCoroutine);   
        //    //逐渐旋转至目标角度
        //    lastRotationCoroutine = StartCoroutine(GraduallyRotateToTarget(targetRotation));
        //}


        //private IEnumerator GraduallyRotateToTarget(Quaternion targetRotation)
        //{
        //    while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        //    {
        //        //逐渐旋转到目标旋转角度
        //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //        yield return null;
        //    }
        //    transform.rotation = targetRotation;
        //}
        #endregion
    }
}