using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieArmy.UI;

namespace ZombieArmy.Skill
{
	/// <summary>
	/// 技能管理器：用于储存，释放队伍内所有成员技能
	/// </summary>
	public class SkillManager : MonoBehaviour
	{
		private Skill[] skills;

        [SerializeField] private float coolDownTime = 30f;
        //冷却剩余时间
        private float coolRemain;
        //对应释放技能的ui按钮
        [SerializeField]private Button skillButton;

        private CoolDownBarController coolDownBarController;

        private void Awake()
        {
			skills = GetComponentsInChildren<Skill>();
            skillButton.onClick.AddListener(() => ReleaseSkills());
            coolDownBarController = skillButton.GetComponent<CoolDownBarController>();
        }

		public void ReleaseSkills()
        {
            if (coolRemain > 0) return;

            foreach (var skill in skills)
            {
                skill.ExecuteSkill();
            }
            //开始技能冷却
            StartCoroutine(CoolDownTimeDecrease());
        }

        private IEnumerator CoolDownTimeDecrease()
        {
            coolRemain = coolDownTime;
            coolDownBarController.OnCoolDownTimeDecreased(coolRemain / coolDownTime);
            while (coolRemain > 0) 
            {
                yield return new WaitForSeconds(1);
                coolRemain--;
                coolDownBarController.OnCoolDownTimeDecreased(coolRemain / coolDownTime);
            }
        }

        private void OnDisable()
        {
            skillButton.onClick.RemoveAllListeners();
        }
    }
}