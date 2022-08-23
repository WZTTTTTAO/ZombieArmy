using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;
using Random = UnityEngine.Random;
using UnityEngine.AI;

namespace ZombieArmy.Character
{

    public class spine : MonoBehaviour { 
    public float MarshDamge;
        private TrapManager TrapManager;
        private void Start()
        {
            TrapManager = FindObjectOfType<TrapManager>();
        }


        private void OnTriggerStay(Collider other)
        {
            if (TrapManager.MarshType == 1)
            {
                other.GetComponent<CharacterStatus>()?.TakeDamage(MarshDamge);

            }
        }

    }
}
