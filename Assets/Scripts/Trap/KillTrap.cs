using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieArmy.Character
{
    public class KillTrap : MonoBehaviour
    {
        public float KillTrapDamge;
        private TrapManager TrapManager;
        private void Start()
        {
            TrapManager = FindObjectOfType<TrapManager>();
        }
        private void OnTriggerStay(Collider other)
        {
            if (TrapManager.KilltrapType == 1)
            {
                if (other.CompareTag("Zombie"))
                {
                    other.GetComponent<ZombieStatus>().currentHealth -= KillTrapDamge;

                }
                if (other.CompareTag("Student"))
                {
                    other.GetComponent<StudentStatus>().currentHealth -= KillTrapDamge;
                }
            }
        }
    }
}
