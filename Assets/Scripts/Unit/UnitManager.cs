using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ZombieArmy.Unit
{
	/// <summary>
	/// 编队管理器
	/// </summary>
	public class UnitManager : MonoSingleton<UnitManager>
	{
        /// 当前选中的单位
        //public CharacterMotor[] currentSelectedUnits { get; private set; }

        public UnitGroup currentSelectedUnitsGroup { get; private set; }

        private Vector3 centralPoint;
		//初始控制单位
		[SerializeField] private Transform initialUnitsGroup;

        public bool unitsCanAttack;

        private void Start()
        {
            currentSelectedUnitsGroup = new UnitGroup(initialUnitsGroup);
            //currentSelectedUnits = GetUnitsByTransParent(initialUnitsGroup);
            RegisterArrivalEventForCurrentSelectedUnits();
        }

        //private void OnDisable()
        //{
        //    foreach (var unitMotor in currentSelectedUnits)
        //    {
        //        unitMotor.UnregisterArriveEvent();
        //    }
        //}

        public UnitGroup ChangeSelectedUnits(Transform unitsGroupParent)
        {
            UnregisterArrivalEventForCurrentSelectedUnits();
            currentSelectedUnitsGroup.UpdateUnitsGroup(unitsGroupParent);
            //currentSelectedUnits = GetUnitsByTransParent(unitsGroup);
            RegisterArrivalEventForCurrentSelectedUnits();

            return currentSelectedUnitsGroup;
        }

        /// <summary>
        /// 为所有当前选中单位注册到达目的地事件
        /// </summary>
        private void RegisterArrivalEventForCurrentSelectedUnits()
        {
            foreach (var unitMotor in currentSelectedUnitsGroup.groupMotors)
            {
                unitMotor.RegisterArriveEvent();
                //显示选中图标
                unitMotor.SetSelectedIconVisibility(true);
            }
        }

        /// <summary>
        /// 为所有当前选中单位注销到达目的地事件
        /// </summary>
        private void UnregisterArrivalEventForCurrentSelectedUnits()
        {
            //如果当前选中目标队伍全部被销毁 则不需要注销事件
            if (currentSelectedUnitsGroup.groupMotors == null) return;

            foreach (var unitMotor in currentSelectedUnitsGroup.groupMotors)
            {
                unitMotor.UnregisterArriveEvent();
                //隐藏选中图标
                unitMotor.SetSelectedIconVisibility(false);
            }
        }

        /// <summary>
        /// 将当前部队移动到鼠标位置
        /// </summary>
        public void MoveCurrentUnitsToMousePosition()
        {
            Ray mouseInputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseInputRay, out hit))
            {
                Vector3 moveToPosition = hit.point;

                foreach (var motor in currentSelectedUnitsGroup.groupMotors)
                {
                    motor.MoveToTargetPosition(moveToPosition);
                }
            }
        }

        


    }
}