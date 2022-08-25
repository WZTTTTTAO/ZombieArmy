using System;
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
		[SerializeField] protected float currentHealth;
        //扣血事件
        public Action<float> OnDamaged;

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

        protected virtual void OnDeath()
        {
            OnDamaged = null;
        }

        protected virtual void OnDamage()
        {
            //血条UI扣血
            OnDamaged?.Invoke(currentHealth / characterStatusInfo.MaxHealth);
        }

        private void Death()
        {
            Destroy(gameObject, 0.1f);
        }
    }
}