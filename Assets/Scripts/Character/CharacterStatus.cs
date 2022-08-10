using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.UI;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 角色状态类：血量、攻击力、战斗数值计算
	/// </summary>
	public class CharacterStatus : MonoBehaviour
	{
		//角色状态信息
		public CharacterStatusInfo characterStatusInfo;
		//血量
		public float currentHealth;

        //角色所处队伍UI
        public Draggable unitGroupUI { get; set; }


        private void Awake()
        {
            currentHealth = characterStatusInfo.Health;
        }

        /// <summary>
        /// 角色受伤减血
        /// </summary>
        /// <param name="amount">扣除血量数</param>
        public void TakeDamage(float amount)
        {
			currentHealth -= amount;
			if (currentHealth <= 0)
            {
                RemoveSelfFromUnitsGroup();
                
                Death();
            }
        }

        //在销毁物体前 将自身从当前队伍中移除
        private void RemoveSelfFromUnitsGroup()
        {
            Transform groupParent = transform.parent;
            //先储存父物体 再将父物体设为空 使FormationManager通过查找子物体更新队伍成员时 查找不到该物体
            transform.parent = null;
            //FormationManager根据父物体更新新的成员
            FormationManager.Instance.currentSelectedUnitsGroup.UpdateUnitsGroup(groupParent);

            //如果队伍成员全部死亡
            if (groupParent.childCount == 0)
            {
                //将控制队伍移动的UI关闭
                unitGroupUI.gameObject.SetActive(false);
            }
            
        }

        private void Death()
        {
			Destroy(gameObject);
        }


        //显示攻击范围
        private void OnDrawGizmos()
        {
           
            Gizmos.DrawWireSphere(transform.position, characterStatusInfo.AttackRange);
        }
    }
}