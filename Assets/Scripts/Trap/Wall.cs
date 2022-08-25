using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieArmy.Character
{
    public class Wall : BaseStatus
    {
        public BaseStatus baseStatus;
        private float studentHp;
        [SerializeField] private float nowHp;
        private Animator anim;

        private void Start()
        {
            studentHp = GetComponent<StudentStatus>().characterStatusInfo.MaxHealth;
            anim = GetComponent<Animator>();
        }
        void Update()
        {
            nowHp = currentHealth;
            if(studentHp>nowHp)
            {
                anim.SetBool("Fall", true);
            }
        }
    }
}