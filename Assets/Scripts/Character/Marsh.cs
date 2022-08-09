using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ZombieArmy.Character
{
    public class Marsh : MonoBehaviour
    {
        public float marshSlowd = 0.2f;
        private bool isLeave, inMarsh;
        private float LeaveTime = 2f;
        private NavMeshAgent nav;
        private float OriginSpeed, NowSpeed;
        private void Awake()
        {
            nav  = FindObjectOfType<NavMeshAgent>();
            OriginSpeed = nav.speed;
            NowSpeed = nav.speed * marshSlowd;
        }

        private  void Update()
        {
            if(isLeave )
            {
                if (nav.speed <OriginSpeed )
                {
                    nav.speed += Time.deltaTime * (OriginSpeed - NowSpeed) / LeaveTime;
                }
                LeaveTime -= Time.deltaTime;
            }
            if (LeaveTime <=0&&isLeave )
            {
                isLeave = false;
                LeaveTime = 2f;
                nav.speed = OriginSpeed;

            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Zombie")||other.CompareTag("Student") && !isLeave && !inMarsh)
            {
                inMarsh = true;
                other.GetComponent<NavMeshAgent>().speed  = NowSpeed ;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Zombie")||other .CompareTag("Student"))
            {
                inMarsh = false;
                isLeave = true;
            }
        }
    }
}
