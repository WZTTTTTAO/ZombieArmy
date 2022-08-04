using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZombieArmy.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class SplitArmySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            //如果部队Ui被拖拽到slot上
            if (eventData.pointerDrag != null)
            {
                
            }
        }
    }
}