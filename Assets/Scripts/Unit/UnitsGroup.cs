using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using ZombieArmy.Character;

namespace ZombieArmy.Unit
{
	/// <summary>
	/// 
	/// </summary>
	public class UnitGroup
	{
		public Transform groupParent;

		public Vector3 centralPoint;

		public CharacterMotor[] groupMotors;

        public float maxDistance;

        public UnitGroup(Transform groupParent)
        {
            UpdateUnitsGroup(groupParent);
        }

        public void UpdateUnitsGroup(Transform groupParent)
        {
            this.groupParent = groupParent;
            this.groupMotors = GetUnitsByTransParent(groupParent);
            if (groupMotors == null) return;
            UpdateCentralPointAndMaxDistance();
        }

        /// <summary>
        /// 计算当前选中队伍的中心点位置 以及队员间最大距离
        /// </summary>
        public void UpdateCentralPointAndMaxDistance()
        {
            float xMin = groupMotors.GetMin(g => g.transform.position.x).transform.position.x;
            float xMax = groupMotors.GetMax(g => g.transform.position.x).transform.position.x;
            float yMin = groupMotors.GetMin(g => g.transform.position.x).transform.position.y;
            float yMax = groupMotors.GetMax(g => g.transform.position.x).transform.position.y;
            float zMin = groupMotors.GetMax(g => g.transform.position.x).transform.position.z;
            float zMax = groupMotors.GetMax(g => g.transform.position.x).transform.position.z;

            maxDistance = (xMax - xMin) > (zMax - zMin) ? (xMax - xMin) : (zMax - zMin);
            centralPoint = new Vector3((xMin + xMax) * 0.5f, (yMin + yMax) * 0.5f, (zMin + zMax) * 0.5f);
        }

        /// <summary>
        /// 根据父物体获取 单位组别
        /// </summary>
        /// <param name="unitsGroupParent">父物体</param>
        /// <returns></returns>
        private CharacterMotor[] GetUnitsByTransParent(Transform unitsGroupParent)
        {
            //如果父物体下小队成员全部被销毁 则返回空的数组
            if (unitsGroupParent.childCount == 0) return null;

            CharacterMotor[] motors = new CharacterMotor[unitsGroupParent.childCount];
            for (int i = 0; i < unitsGroupParent.childCount; i++)
            {
                motors[i] = unitsGroupParent.GetChild(i).GetComponent<CharacterMotor>();
            }
            return motors;
        }

    }
}