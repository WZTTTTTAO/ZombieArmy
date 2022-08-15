using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.Unit;
using Random = UnityEngine.Random;
using UnityEngine.AI;
namespace ZombieArmy.Character
{
    public class MarshTest : MonoBehaviour
    {
        public float marshSlowd = 0.2f;
        public float MarshDamge;
        private void Awake()
        {
            // nav = FindObjectOfType<NavMeshAgent>();
            // Debug.Log(nav.gameObject.name);
            // OriginSpeed = nav.speed;
            //NowSpeed = nav.speed * marshSlowd;

        }

        private void Update()
        {

          //  if (isLeave)
        //    {
         //       if (nav.speed < OriginSpeed)
          //      {
        //            nav.speed += Time.deltaTime * (OriginSpeed - NowSpeed) / LeaveTime;
        //
            //    }
         //       LeaveTime -= Time.deltaTime;
          //  }
         //   if (LeaveTime <= 0 && isLeave)
          //  {
         //       isLeave = false;
         //       LeaveTime = 2f;
           //     nav.speed = OriginSpeed;


          //  }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Zombie") || other.CompareTag("Student"))
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * marshSlowd;

            }
        }
        private void OnTriggerStay(Collider other)
       {

           if (other.CompareTag("Zombie")  )
           {
                other.GetComponent<ZombieStatus>().currentHealth -= MarshDamge;
                      
           }
           if (other.CompareTag("Student"))
            {
                other.GetComponent<CharacterStatus>().currentHealth -= MarshDamge;
            }
      }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Zombie") || other.CompareTag("Student"))
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed / marshSlowd;
            }
        }
    }
}
