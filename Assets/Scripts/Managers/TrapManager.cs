using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ZombieArmy
{
    public class TrapManager : MonoBehaviour
    {
        private float ChangeTime, KillGapTime, KilltrapLastTime;
        public int MarshType = 1;
        public  int KilltrapType = 0;
        public float MarshChangeTime, KilltrapChangeTime, KiltrapLast;
        void Update()
        {
            if (KilltrapType == 1)
            {
                KilltrapLastTime += Time.deltaTime;
                if (KilltrapLastTime >= 1)
                {
                    KilltrapType = 0;
                }
            }
            if (Input.GetButtonDown("TrapChange") && ChangeTime > MarshChangeTime)
            {
                if (MarshType == 3)
                {
                    MarshType = 1;
                    ChangeTime = 0;
                }
                else
                {
                    MarshType++;
                    ChangeTime = 0;
                }
            }
            if (Input.GetButtonDown("KillTrap") && KillGapTime > KilltrapChangeTime)
            {
                if (KilltrapType == 0)
                {
                    KilltrapLastTime = 0;
                    KilltrapType = 1;
                }
            }
            ChangeTime += Time.deltaTime;
            KillGapTime += Time.deltaTime;

        }
    }
}