using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ZombieArmy.Character
{


    public class MarshCharacter : MonoBehaviour
    {
        private NavMeshAgent nav;
        public float marshSlowd = 0.2f;
        private float Speed;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            Speed = nav.speed;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Marsh")
            {
                nav.speed = Speed * marshSlowd;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Marsh")
            {
                nav.speed = Speed;
            }
        }
    }
}