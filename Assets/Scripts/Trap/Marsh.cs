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
        public float marshFast;
        public float MarshDamge;
        public int ABC;
        private TrapManager TrapManager;
        private void Start()
        {
            TrapManager = FindObjectOfType<TrapManager>();
        }
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
            if (TrapManager .MarshType ==2)
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * marshSlowd;
                Debug.Log(2);

            }
             if (TrapManager .MarshType ==3)
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * marshFast;
                Debug.Log(3);
            }
        }
        private void OnTriggerStay(Collider other)
       {
            if (TrapManager.MarshType == 1)
            {
                if (other.CompareTag("Zombie"))
                {
                    other.GetComponent<ZombieStatus>().currentHealth -= MarshDamge;

                }
                if (other.CompareTag("Student"))
                {
                    other.GetComponent<StudentStatus>().currentHealth -= MarshDamge;
                }
            }
      }

        private void OnTriggerExit(Collider other)
        {
            if (TrapManager.MarshType == 2)
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed / marshSlowd;
            }
            if (TrapManager.MarshType == 3)
            {
                other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed /marshFast;
            }
        }
    }
}
