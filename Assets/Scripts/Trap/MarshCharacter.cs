using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ZombieArmy.Character
{


    public class MarshCharacter : MonoBehaviour
    {
        
        public float MarshDamge;//陷阱伤害
        private NavMeshAgent nav;
        public float marshSlowd = 0.2f;//减速系数
        private float Speed;
        private CharacterStatus Status;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            Speed = nav.speed;
            Status = GetComponent<CharacterStatus>();

        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision .gameObject .tag =="Marsh")
            {
<<<<<<< HEAD
                nav.speed = Speed * marshSlowd;//减速
        
=======
                nav.speed = Speed * marshSlowd;
                Debug.Log("进入");
>>>>>>> afa92b36c31457640cbcde659fcbfed9e018e762
            }

        }
        private void OnCollisionStay(Collision collision)
        {
            float DamgeTime = 0;
            float GapTime = 1; 
            if (collision .gameObject .tag =="Marsh")
            {
                if (DamgeTime == 0)
               {
                    Status.currentHealth -= MarshDamge;
              }
                else if (GapTime < DamgeTime)
               {
                    DamgeTime = 0;                    
                }
               else
               {
                    DamgeTime += Time.deltaTime;
               }
               
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision  .gameObject .tag == "Marsh")
            {
                nav.speed = Speed;//恢复速度
            }
        }
    }
}