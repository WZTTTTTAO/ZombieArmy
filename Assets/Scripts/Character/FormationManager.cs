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
            CharacterMotor[] motors = new CharacterMotor[initialUnitsGroup.childCount];
            for (int i = 0; i < initialUnitsGroup.childCount; i++)
            {
                motors[i] = initialUnitsGroup.GetChild(i).GetComponent<CharacterMotor>();
            }
            currentSelectedUnits = motors;
            RegisterArrivalEventForCurrentSelectedUnits();
        }

        private void OnDisable()
        {
            foreach (var unitMotor in currentSelectedUnits)
            {
                unitMotor.UnregisterArriveEvent();
            }
        }

        public void ChangeSelectedUnits(CharacterMotor[] newUnits)
        {
            UnregisterArrivalEventForCurrentSelectedUnits();
            currentSelectedUnits = newUnits;
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

    }
}