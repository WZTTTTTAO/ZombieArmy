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
	public class CharacterStatus : BaseStatus
	{
		
        //首击反馈
        private MMF_Player hitFeedbacksPlayer;
        //死亡反馈
        private MMF_Player deathFeedbacksPlayer;

        public event Action<float> OnDamaged;

        private new void Awake()
        {
            base.Awake();
            hitFeedbacksPlayer = transform.Find("hit fb").GetComponent<MMF_Player>();
            deathFeedbacksPlayer = transform.Find("death fb").GetComponent<MMF_Player>();
        }

        protected override void OnDamage()
        {
            base.OnDamage();
            //播放所有受击反馈效果
            hitFeedbacksPlayer?.PlayFeedbacks();
            //血条UI扣血
            OnDamaged?.Invoke(currentHealth / characterStatusInfo.MaxHealth);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            deathFeedbacksPlayer?.PlayFeedbacks(transform.position);
        }


        //显示攻击范围
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, characterStatusInfo.AttackRange);
        }
    }
}