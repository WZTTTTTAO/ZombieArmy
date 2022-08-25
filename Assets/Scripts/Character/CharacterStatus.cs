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

        protected new void Awake()
        {
            base.Awake();
            hitFeedbacksPlayer = transform.Find("hit fb").GetComponent<MMF_Player>();
        }

        protected override void OnDamage()
        {
            base.OnDamage();
            //播放所有受击反馈效果
            hitFeedbacksPlayer?.PlayFeedbacks();
            
        }

       
    }
}