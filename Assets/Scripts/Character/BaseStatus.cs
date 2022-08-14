using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 
	/// </summary>
	public class BaseStatus : MonoBehaviour
	{
		//角色状态信息
		public CharacterStatusInfo characterStatusInfo;
		//血量
		public float currentHealth;

        protected void Awake()
        {
            currentHealth = characterStatusInfo.MaxHealth;
		}

        /// <summary>
        /// 角色受伤减血
        /// </summary>
        /// <param name="amount">扣除血量数</param>
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            OnDamage();
           
            if (currentHealth <= 0)
            {
                OnDeath();
                Death();
            }
        }

        protected virtual void OnDeath() { }
        protected virtual void OnDamage() { }

        private void Death()
        {
            Destroy(gameObject, 0.1f);
        }
    }
}