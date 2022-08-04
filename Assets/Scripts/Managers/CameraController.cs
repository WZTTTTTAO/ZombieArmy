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
		[SerializeField] private float panSpeed = 20f;
        [SerializeField] private float screenPanBorderThickness = 10f;

        [SerializeField] private float scrollSpeed = 20f;
        private float scroll;
        [SerializeField] private float maxScrollSize = 30;
        [SerializeField] private float minScrollSize = 5;

        private void Update()
        {
            Vector3 movePosition = transform.position;

            if (Input.mousePosition.y >= Screen.height - screenPanBorderThickness)
            {
                movePosition += Vector3.forward * panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.y < screenPanBorderThickness)
            {
                movePosition += -Vector3.forward * panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.x >= Screen.width - screenPanBorderThickness)
            {
                movePosition += Vector3.right * panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.x < screenPanBorderThickness)
            {
                 movePosition += -Vector3.right * panSpeed * Time.deltaTime;
            }

            scroll = Input.GetAxis("Mouse ScrollWheel");

            movePosition += Vector3.up * scroll * scrollSpeed * 100f * Time.deltaTime;

            movePosition.y = Mathf.Clamp(movePosition.y, minScrollSize, maxScrollSize);

            transform.position = movePosition;


        }

        
	}
}