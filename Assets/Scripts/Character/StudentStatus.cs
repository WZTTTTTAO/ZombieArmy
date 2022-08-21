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
    public class StudentStatus : CharacterStatus
    {
        public event EventHandler StudentDeath;//声明事件
        protected override void OnDeath()
        {
            base.OnDeath();
            DeathEvent(gameObject, EventArgs.Empty);
        }

        //显示侦测范围
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, GetComponent<StudentAi>().DetectionRange);
        }

        public void DeathEvent(GameObject go, EventArgs e)  
        {
            if (StudentDeath != null)
            {
                StudentDeath(go, e);
            }
        }
    }
}
        