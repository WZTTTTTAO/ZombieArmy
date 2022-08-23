using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;
using Random = UnityEngine.Random;
using UnityEngine.AI;

namespace ZombieArmy.Character
{
    public class AccelerationBelt : MonoBehaviour
    {
            public float marshFast;

            private void OnTriggerEnter(Collider other)
            {
           
                    other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * marshFast;

            }
           

            private void OnTriggerExit(Collider other)
            {
               
                    other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed / marshFast;
                
            }
        }
    
}
