using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using ZombieArmy.Character;
using ZombieArmy.UI;

namespace ZombieArmy.Unit
{
	/// <summary>
	/// 
	/// </summary>
	public class UnitSpawner : MonoSingleton<UnitSpawner>
	{
		//[SerializeField] private Transform[] spawnPositions;

        [SerializeField] private GameObject healthBarPrefab;

        [SerializeField] private RectTransform canvasTrans;

        [SerializeField] private GameObject meleeZombiePrefab;
        [SerializeField] private Transform meleeZombieGroupTrans;

        public override void Init()
        {
            base.Init();
			SetupHealthBarUI();
        }
      
        private void SetupHealthBarUI()
        {
            //for (int i = 0; i < spawnPositions.Length; i++)
            //{
            //    for (int j = 0; j < spawnPositions[i].childCount; j++)
            //    {
            //        GameObject healthBarGO = Instantiate(healthBarPrefab);
            //        healthBarGO.transform.SetParent(canvasTrans);

            //        HealthBarController healthBarController = healthBarGO.GetComponent<HealthBarController>();
            //        healthBarController.SetCanvasTrans(canvasTrans);
            //        healthBarController.SetTarget(spawnPositions[i].GetChild(j), 2f);
            //        spawnPositions[i].GetChild(j).GetComponent<CharacterStatus>().OnDamaged = healthBarController.OnCharacterDamaged;
            //    }
            //}

            foreach (var baseStatus in FindObjectsOfType<BaseStatus>())
            {
                GameObject healthBarGO = Instantiate(healthBarPrefab);
                healthBarGO.transform.SetParent(canvasTrans);

                HealthBarController healthBarController = healthBarGO.GetComponent<HealthBarController>();
                healthBarController.SetCanvasTrans(canvasTrans);
                healthBarController.SetTarget(baseStatus.transform, 2f);
                baseStatus.OnDamaged = healthBarController.OnCharacterDamaged;
            }
        }

        public void AddUnitToUnitGroup(Transform spawnPoint)
        {
            GameObject zombieGO = Instantiate(meleeZombiePrefab, spawnPoint.position, Quaternion.identity);
            zombieGO.transform.parent = meleeZombieGroupTrans;
            UnitManager.Instance.ChangeSelectedUnits(UnitManager.Instance.currentSelectedUnitsGroup.groupParent);
        }
    }
}