using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;
using Random = UnityEngine.Random;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 僵尸AI
	/// </summary>
	public class ZombieAI : MonoBehaviour
	{
        //攻击的目标
		[SerializeField] private Transform attackTarget;
        //敌人layer
        [SerializeField] private LayerMask enemyLayer;
        //角色状态信息类对象
        public CharacterStatusInfo characterStatusInfoInstance { get; set; }
        //角色马达
        private CharacterMotor motor;

        //开始攻击时间
        private float startAttackTime;

        //攻击范围内的所有敌人目标 最多10个敌人
        public Collider[] withinAttackRangeEnemies = new Collider[10];

        private void Awake()
        {
            characterStatusInfoInstance = Instantiate(GetComponent<CharacterStatus>().characterStatusInfo);
            motor = GetComponent<CharacterMotor>();
            
        }

        private void Update()
        {
            //检测攻击范围内的敌人
            int overlapEnemyCount = Physics.OverlapSphereNonAlloc(transform.position, characterStatusInfoInstance.AttackRange, withinAttackRangeEnemies, enemyLayer);
            //withinAttackRangeEnemies = Physics.OverlapSphere(transform.position, characterStatusInfo.AttackRange, enemyLayer);

            motor.SetNavAgentStopped(false);

            //如果攻击范围内有敌人 则停止寻路并攻击敌人 (如果被强制停止攻击，则停止攻击)
            if (overlapEnemyCount == 0 || !UnitManager.Instance.unitsCanAttack) return;
            motor.SetNavAgentStopped(true);

            //根据攻击时间间隔计算攻击
            if (startAttackTime < Time.time)
            {
                AttackTargetEnemy(withinAttackRangeEnemies, overlapEnemyCount);
                startAttackTime = Time.time + characterStatusInfoInstance.AttackInterval;
            }
        }

        private void AttackTargetEnemy(Collider[] withinAttackRangeEnemies, int enemyCount = 0)
        {
            //选择目标敌人
            attackTarget = SelectTargetEnemy(withinAttackRangeEnemies, enemyCount);
            //面向目标敌人
            //motor.GraduallyRotateTowardTarget(attackTarget.transform.position);
            //目标敌人扣血
            attackTarget.GetComponent<CharacterStatus>().TakeDamage(characterStatusInfoInstance.Atk);
        }

        /// <summary>
        /// 根据仇恨值选择一个目标敌人
        /// </summary>
        /// <param name="withinAttackRangeEnemies">在攻击范围内的所有敌人数组</param>
        /// <returns></returns>
        private Transform SelectTargetEnemy(Collider[] withinAttackRangeEnemies, int enemyCount = 0)
        {
            //随机一个仇恨值
            float randomHatredValue = Random.Range(0, 1f);
            //计算总仇恨值
            int hatredCount = 0;
            for (int i = 0; i < enemyCount; i++)
            {
                hatredCount += withinAttackRangeEnemies[i].GetComponent<CharacterStatus>().characterStatusInfo.Hatred;
            }

            //计算每一个单位的仇恨值比例 判断随机仇恨值是否在比例区间内
            float hatredRatio = 0;
            for (int i = 0; i < enemyCount; i++)
            {
                //左区间 = 当前仇恨比例， 仇恨比例右区间 = 当前仇恨比例 + 当前敌人仇恨值占比
                float hatredRatioRightRange = hatredRatio + (float)withinAttackRangeEnemies[i].GetComponent<CharacterStatus>().characterStatusInfo.Hatred / hatredCount;

                if (hatredRatio <= randomHatredValue &&
                    randomHatredValue < hatredRatioRightRange)
                {
                    return withinAttackRangeEnemies[i].transform;
                }

                //更新仇恨比例左区间为右区间
                hatredRatio = hatredRatioRightRange;
            }
            return withinAttackRangeEnemies[enemyCount - 1].transform;
        }


    }
}