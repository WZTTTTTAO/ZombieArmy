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

        [SerializeField] private Transform unitsGroup;

        private UnitsGroup currentSelectedUnitsGroup;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponentInChildren<Canvas>();

            //将对应队伍的所有成员 对应的Ui设置为自己
            for (int i = 0; i < unitsGroup.childCount; i++)
            {
                unitsGroup.GetChild(i).GetComponent<CharacterStatus>().unitGroupUI = this;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentSelectedUnitsGroup = FormationManager.Instance.ChangeSelectedUnits(unitsGroup);
            if (currentSelectedUnitsGroup.groupMotors == null) return;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
            beginDragPosition = rectTransform.anchoredPosition;
            CameraController.Instance.isDraggingUI = true;
           
            Bezier.Instance.SetStartPoint(currentSelectedUnitsGroup.centralPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (currentSelectedUnitsGroup.groupMotors == null) return;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            currentSelectedUnitsGroup.UpdateCentralPointAndMaxDistance();
            Bezier.Instance.SetStartPoint(currentSelectedUnitsGroup.centralPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (currentSelectedUnitsGroup.groupMotors == null) return;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            rectTransform.anchoredPosition = beginDragPosition;
            FormationManager.Instance.MoveCurrentUnitsToMousePosition();
            CameraController.Instance.isDraggingUI = false;
            Bezier.Instance.StopDrawingCurve();
        }
    }
}