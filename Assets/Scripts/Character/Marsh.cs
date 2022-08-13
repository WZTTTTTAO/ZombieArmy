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
        public float OriginSpeed, NowSpeed;
        private void Awake()
        {
            nav  = FindObjectOfType<NavMeshAgent>();
            Debug.Log(nav.gameObject.name);
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
                    Debug.Log("!!!!!!!!!!!!!!!!! " + nav.gameObject.name);
                }
                LeaveTime -= Time.deltaTime;
            }
            if (LeaveTime <=0&&isLeave )
            {
                isLeave = false;
                LeaveTime = 2f;
                nav.speed = OriginSpeed;
                Debug.Log("@@@@@@@@@@@@@@@@2 " + nav.gameObject.name);

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
                Debug.Log("11111111111111");
            }
        }
    }
}
