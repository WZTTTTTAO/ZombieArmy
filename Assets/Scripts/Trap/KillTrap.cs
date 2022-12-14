using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieArmy.Character
{
    public class KillTrap : MonoBehaviour
    {
        public float KillTrapDamge;
        private TrapManager TrapManager;
        public KillTrapTrigger killTrapTrigger;
        private void Start()
        {
            TrapManager = FindObjectOfType<TrapManager>();
        }
        private void OnTriggerStay(Collider other)
        {
            if (TrapManager.KilltrapType == 1 && killTrapTrigger.isZombieStay == true)
            {
                other.GetComponent<CharacterStatus>()?.TakeDamage(KillTrapDamge);

            }
        }
    }
}
