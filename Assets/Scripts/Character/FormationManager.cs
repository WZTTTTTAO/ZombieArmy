using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 编队管理器
	/// </summary>
	public class FormationManager : MonoSingleton<FormationManager>
	{
        /// 当前选中的单位
        public CharacterMotor[] currentSelectedUnits { get; private set; }
		//初始控制单位
		[SerializeField] private Transform initialUnitsGroup;

        private void Start()
        {
            currentSelectedUnits = GetUnitsByTransParent(initialUnitsGroup);
            RegisterArrivalEventForCurrentSelectedUnits();
        }

        private CharacterMotor[] GetUnitsByTransParent(Transform unitsGroup)
        {
            CharacterMotor[] motors = new CharacterMotor[unitsGroup.childCount];
            for (int i = 0; i < unitsGroup.childCount; i++)
            {
                motors[i] = unitsGroup.GetChild(i).GetComponent<CharacterMotor>();
            }
            return motors;
        }

        private void OnDisable()
        {
            foreach (var unitMotor in currentSelectedUnits)
            {
                unitMotor.UnregisterArriveEvent();
            }
        }

        public void ChangeSelectedUnits(Transform unitsGroup)
        {
            UnregisterArrivalEventForCurrentSelectedUnits();
            currentSelectedUnits = GetUnitsByTransParent(unitsGroup);
            RegisterArrivalEventForCurrentSelectedUnits();
        }

        /// <summary>
        /// 为所有当前选中单位注册到达目的地事件
        /// </summary>
        private void RegisterArrivalEventForCurrentSelectedUnits()
        {
            foreach (var unitMotor in currentSelectedUnits)
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
            foreach (var unitMotor in currentSelectedUnits)
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

                foreach (var motor in currentSelectedUnits)
                {
                    motor.MoveToTargetPosition(moveToPosition);
                }
            }
        }

    }
}