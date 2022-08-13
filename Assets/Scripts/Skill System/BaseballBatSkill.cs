using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Character;

namespace ZombieArmy.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseballBatSkill : Skill
    {
        private CharacterStatusInfo characterStatusInfoInstance;
        private float baseAttack;
        private float attackIncreasedRatio = 3.3f;
        private float duration = 10f;

        private void Start()
        {
            characterStatusInfoInstance = GetComponent<ZombieAI>().characterStatusInfoInstance;
        }

        public override void ExecuteSkill()
        {
            baseAttack = characterStatusInfoInstance.Atk;
            characterStatusInfoInstance.Atk = baseAttack * attackIncreasedRatio;
            StartCoroutine(ResetAtk(duration));
        }

        private IEnumerator ResetAtk(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            characterStatusInfoInstance.Atk = baseAttack;
        }
    }
}