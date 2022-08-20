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
    public class StudentStatus : MonoBehaviour
    {
        public event EventHandler StudentDeath;//声明事件
        //角色状态信息
        public CharacterStatusInfo characterStatusInfo;
        //血量
        public float currentHealth;
        //首击反馈
        private MMF_Player hitFeedbacksPlayer;

        public event Action<float> OnDamaged;
        public GameObject Zombie;
        private void Awake()
        {
            currentHealth = characterStatusInfo.MaxHealth;
            hitFeedbacksPlayer = transform.Find("hit fb").GetComponent<MMF_Player>();
        }

        /// <summary>
        /// 角色受伤减血
        /// </summary>
        /// <param name="amount">扣除血量数</param>
        private void Update()
        {
            OnDamaged?.Invoke(currentHealth / characterStatusInfo.MaxHealth);
            if (currentHealth <= 0)
            {
                OnDeath();
                Death();
            }
            OnDrawGizmos();
        }
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            //播放所有受击反馈效果
            hitFeedbacksPlayer?.PlayFeedbacks();
            //血条UI扣血
        }


        protected virtual void OnDeath() { }

        private void Death()
        {
            if (gameObject.tag == "Student")
            {
                DeathEvent(gameObject, EventArgs.Empty);
            }
            Destroy(gameObject);
        }


        //显示攻击范围
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
        