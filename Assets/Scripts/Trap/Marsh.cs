using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;
using Random = UnityEngine.Random;
using UnityEngine.AI;

namespace ZombieArmy.Character
{
    public class Marsh : MonoBehaviour
    {
        public float marshSlowd = 0.2f;
        private void OnTriggerEnter(Collider other)
        {  
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * marshSlowd;
        }
      

        private void OnTriggerExit(Collider other)
        {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed / marshSlowd;
        }
    }
}
