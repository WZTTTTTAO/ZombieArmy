using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Character;
using ZombieArmy.UI;

namespace ZombieArmy.Unit
{
	/// <summary>
	/// 
	/// </summary>
	public class UnitSpawner : MonoBehaviour
	{
		[SerializeField] private Transform[] spawnPositions;

        [SerializeField] private GameObject healthBarPrefab;

        [SerializeField] private RectTransform canvasTrans;

        private void Awake()
        {
			SetupHealthBarUI();
        }

        private void SetupHealthBarUI()
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                for (int j = 0; j < spawnPositions[i].childCount; j++)
                {
                    GameObject healthBarGO = Instantiate(healthBarPrefab);
                    healthBarGO.transform.SetParent(canvasTrans);

                    HealthBarController healthBarController = healthBarGO.GetComponent<HealthBarController>();
                    healthBarController.SetCanvasTrans(canvasTrans);
                    healthBarController.SetTarget(spawnPositions[i].GetChild(j), 2f);
                    spawnPositions[i].GetChild(j).GetComponent<CharacterStatus>().OnDamaged += healthBarController.OnCharacterDamaged;
                }
            }
        }

       
    }
}