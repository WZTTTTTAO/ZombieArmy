using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieArmy.UI
{
	/// <summary>
	/// 血条UI控制器：在角色上方显示血条UI，实时按需更新血条位置
	/// </summary>
	public class HealthBarController : MonoBehaviour
	{
		private RectTransform rectTransform;

		//显示血条的目标角色
		private Transform targetCharacter;

		private RectTransform canvasRectTrans;

		private float yOffset;

		private Vector3 lastTargetPosition;

		[SerializeField] private Image healthImg;

		[SerializeField] private Color fullHealthColor = Color.green;
		[SerializeField] private Color damagedColor = Color.green;
		[SerializeField] private Color lowHealthColor = Color.red;

		private void Awake()
        {
			rectTransform = GetComponent<RectTransform>();
			healthImg.color = fullHealthColor;
        }

        private void LateUpdate()
        {
			if (!targetCharacter
				//|| lastTargetPosition == targetCharacter.position
				)
				return;

			SetHealthBarPosition();
        }

        private void SetHealthBarPosition()
        {
			if (!targetCharacter || !canvasRectTrans) return;

			Vector2 viewportPos = Camera.main.WorldToViewportPoint(new Vector3(targetCharacter.position.x, targetCharacter.position.y + yOffset, targetCharacter.position.z));
			Vector2 targetCanvasPos = new Vector2(
			(viewportPos.x * canvasRectTrans.sizeDelta.x) - (canvasRectTrans.sizeDelta.x * 0.5f),
			(viewportPos.y * canvasRectTrans.sizeDelta.y) - (canvasRectTrans.sizeDelta.y * 0.5f));

			rectTransform.anchoredPosition = targetCanvasPos;

			lastTargetPosition = targetCharacter.position;
		}

		public void SetTarget(Transform	target, float yOffset)
        {
			targetCharacter = target;
			this.yOffset = yOffset;
			SetHealthBarPosition();
		}

		public void SetCanvasTrans(RectTransform canvasTrans)
		{
			canvasRectTrans = canvasTrans;
		}

		public void OnCharacterDamaged(float healthRatio)
        {
			healthImg.fillAmount = healthRatio;

			if (healthRatio <= 0)
				Destroy(gameObject);
			else if (healthRatio <= 0.2f)
				healthImg.color = lowHealthColor;
			else if (healthRatio <= 0.6f)
				healthImg.color = damagedColor;
		}
	}
}