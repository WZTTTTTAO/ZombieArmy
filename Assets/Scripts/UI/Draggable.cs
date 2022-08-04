using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ZombieArmy.Character;

namespace ZombieArmy.UI
{
    /// <summary>
    /// 可被拖动UI
    /// </summary>
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;

        private Vector3 beginDragPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponentInChildren<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
            beginDragPosition = rectTransform.anchoredPosition;
            CameraController.Instance.isDraggingUI = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            rectTransform.anchoredPosition = beginDragPosition;
            CharacterInputController.Instance.MoveCurrentUnitsToMousePosition();
            CameraController.Instance.isDraggingUI = false;
        }
    }
}