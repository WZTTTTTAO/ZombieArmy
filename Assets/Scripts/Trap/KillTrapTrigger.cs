using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieArmy
{
    public class KillTrapTrigger : MonoBehaviour
    {
        private   int zombieNumber;
        public float trapRange;
        public LayerMask enemyLayer;
        private   Collider[] inTriggerZombie = new Collider[10];
        public bool isZombieStay=false;
        private void Update()
        {
            zombieNumber = Physics.OverlapSphereNonAlloc(transform.position, trapRange,inTriggerZombie,enemyLayer);
            if(zombieNumber ==0)
            {
                isZombieStay = false;
            }
            else
            {
                isZombieStay = true;
            }
        }

    }
}