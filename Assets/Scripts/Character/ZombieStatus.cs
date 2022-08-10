using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieArmy.UI;

namespace ZombieArmy.Character
{
	/// <summary>
	/// 僵尸状态类
	/// </summary>
	public class ZombieStatus : CharacterStatus
	{
		//角色所处队伍UI
		public Draggable unitGroupUI { get; set; }

        protected override void OnDeath()
        {
            base.OnDeath();
            RemoveSelfFromUnitsGroup();
        }

        //在销毁物体前 将自身从当前队伍中移除
        private void RemoveSelfFromUnitsGroup()
        {
            Transform groupParent = transform.parent;
            //先储存父物体 再将父物体设为空 使FormationManager通过查找子物体更新队伍成员时 查找不到该物体
            transform.parent = null;
            //FormationManager根据父物体更新新的成员
            FormationManager.Instance.currentSelectedUnitsGroup.UpdateUnitsGroup(groupParent);

            //如果队伍成员全部死亡
            if (groupParent.childCount == 0)
            {
                //将控制队伍移动的UI关闭
                unitGroupUI.gameObject.SetActive(false);
            }

        }
    }
}