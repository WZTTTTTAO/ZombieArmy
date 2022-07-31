using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				Death();
        }

        private void Death()
        {
			Destroy(gameObject);
        }
    }
}