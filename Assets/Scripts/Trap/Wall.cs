using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieArmy.Character
{
    public class Wall : BaseStatus
    {
        public BaseStatus baseStatus;
        public float studentHp=10000;
        [SerializeField] private float nowHp;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        void Update()
        {
            nowHp = currentHealth;
            if(studentHp>nowHp)
            {
                anim.SetBool("Fall", true);
                Debug.Log(123);
            }
        }
    }
}