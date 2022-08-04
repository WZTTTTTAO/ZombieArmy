using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ZombieArmy
{
	/// <summary>
	/// 相机控制器
	/// </summary>
	public class CameraController : MonoSingleton<CameraController>
	{
        private Vector3 movePosition;

        [SerializeField] private float panSpeed = 20f;
        [SerializeField] private float screenPanBorderThickness = 10f;

        [SerializeField] private float scrollSpeed = 20f;
        private float scroll;
        [SerializeField] private float maxScrollSize = 30;
        [SerializeField] private float minScrollSize = 5;

        //是否正在拖动UI
        public bool isDraggingUI { get; set; }

        //可以拖拽屏幕进行移动的区域
        [SerializeField] private RectTransform screenDragArea;
        //是否正在拖动屏幕
        public bool isDraggingScreen = false;
        private Vector3 screenDragOriginalPosition;
        private Vector3 screenDragDifference;
        private Vector3 lastScreenDragPosition;

        [SerializeField] private float dragInterval = 0.2f;
        private float startDragTime;

        private new Camera camera;

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            movePosition = transform.position;

            //如果正在拖动UI 则根据鼠标位置移动相机
            if (isDraggingUI)
                MousePositionPanningControl();
            //如果没有拖动UI 则根据拖拽屏幕和滚轮操作移动相机
            else
            {
                MouseScrollingControl();

                ScreenDraggingControl();
            }

            movePosition.y = Mathf.Clamp(movePosition.y, minScrollSize, maxScrollSize);

            transform.position = movePosition;
        }

        int count;
        //屏幕拖拽控制
        private void ScreenDraggingControl()
        {
            //按下鼠标后 如果鼠标在拖拽区域范围内 则记录起始位置 并可以拖拽
            if (Input.GetMouseButtonDown(1))
            {
                if(RectTransformUtility.RectangleContainsScreenPoint(screenDragArea, Input.mousePosition))
                {
                    screenDragOriginalPosition = Input.mousePosition;
                    isDraggingScreen = true;
                }
            }

            //如果鼠标没有按下 则不更新相机位置
            if (!Input.GetMouseButton(1)){ startDragTime = 0; isDraggingScreen = false; return; }
            //如果没有拖拽屏幕 则不更新相机位置
            if (!isDraggingScreen) return;
            //如果鼠标拖拽后停着不动 则不更新相机位置
            if ((Input.mousePosition - lastScreenDragPosition).sqrMagnitude < 0.1f) { startDragTime = 0; isDraggingScreen = false; return; }

            //根据和起始点的差 更新相机位置
            screenDragDifference = camera.ScreenToViewportPoint(Input.mousePosition - screenDragOriginalPosition);
            UpdateCameraPanningPosition(new Vector3(-screenDragDifference.x, 0, -screenDragDifference.y).normalized);

            //每间隔0.2秒 记录上一次鼠标位置
            startDragTime += Time.deltaTime;
            if (startDragTime > dragInterval)
            {
                lastScreenDragPosition = Input.mousePosition;
                startDragTime = 0;
            }
        }

        //鼠标滚轮控制
        private void MouseScrollingControl()
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
            movePosition += Vector3.up * scroll * scrollSpeed * 100f * Time.deltaTime;
        }

        //鼠标移动控制
        private void MousePositionPanningControl()
        {
            if (Input.mousePosition.y >= Screen.height - screenPanBorderThickness)
            {
                UpdateCameraPanningPosition(Vector3.forward);
            }
            else if (Input.mousePosition.y < screenPanBorderThickness)
            {
                UpdateCameraPanningPosition(-Vector3.forward);
            }
            else if (Input.mousePosition.x >= Screen.width - screenPanBorderThickness)
            {
                UpdateCameraPanningPosition(Vector3.right);
            }
            else if (Input.mousePosition.x < screenPanBorderThickness)
            {
                UpdateCameraPanningPosition(-Vector3.right);
            }

        }

        //更新相机位置
        public void UpdateCameraPanningPosition(Vector3 direction)
        {
            movePosition += direction * panSpeed * Time.deltaTime;
        }

    }
}