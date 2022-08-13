using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
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
        //首击反馈
        private MMF_Player hitFeedbacksPlayer;

        public event Action<float> OnDamaged;

        private void Awake()
        {
            currentHealth = characterStatusInfo.Health;
            hitFeedbacksPlayer = transform.Find("hit fb").GetComponent<MMF_Player>();
        }

        /// <summary>
        /// 角色受伤减血
        /// </summary>
        /// <param name="amount">扣除血量数</param>
        public void TakeDamage(float amount)
        {
			currentHealth -= amount;
            //播放所有受击反馈效果
            hitFeedbacksPlayer?.PlayFeedbacks();
            OnDamaged?.Invoke(currentHealth / characterStatusInfo.Health);
			if (currentHealth <= 0)
            {
                OnDeath();
                Death();
            }
        }

        protected virtual void OnDeath() { }

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