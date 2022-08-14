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
        //死亡反馈
        private MMF_Player deathFeedbacksPlayer;

        public event Action<float> OnDamaged;

        private void Awake()
        {
            currentHealth = characterStatusInfo.MaxHealth;
            hitFeedbacksPlayer = transform.Find("hit fb").GetComponent<MMF_Player>();
            deathFeedbacksPlayer = transform.Find("death fb").GetComponent<MMF_Player>();
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
            //血条UI扣血
            OnDamaged?.Invoke(currentHealth / characterStatusInfo.MaxHealth);

			if (currentHealth <= 0)
            {
                deathFeedbacksPlayer?.PlayFeedbacks(transform.position);
                OnDeath();
                Death();
            }
        }

        protected virtual void OnDeath() { }

        private void Death()
        {
			Destroy(gameObject,0.1f);
        }


        //显示攻击范围
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, characterStatusInfo.AttackRange);
        }
    }
}