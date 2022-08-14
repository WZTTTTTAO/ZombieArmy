using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieArmy.UI
{
    /// <summary>
    /// 冷却条控制器
    /// </summary>
    public class CoolDownBarController : MonoBehaviour
    {
        [SerializeField] private Image coolDownBarImg;

        [SerializeField] private Color coolingColor;
        [SerializeField] private Color coolingFinishedColor;

        public void OnCoolDownTimeDecreased(float remainRatio)
        {
            if (remainRatio <= 0)
            {
                coolDownBarImg.color = coolingFinishedColor;
                coolDownBarImg.fillAmount = 1;
            }
            else
            {
                coolDownBarImg.color = coolingColor;
                coolDownBarImg.fillAmount = remainRatio;
            }
              
        }
    }
}