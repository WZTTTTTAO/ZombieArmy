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
           
        }

    }
}