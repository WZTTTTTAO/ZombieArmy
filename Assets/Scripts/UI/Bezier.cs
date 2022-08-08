using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using ZombieArmy.Character;

namespace ZombieArmy.UI
{
	/// <summary>
	/// 绘制贝塞尔曲线
	/// </summary>
	public class Bezier : MonoSingleton<Bezier>
	{
        private LineRenderer lineRenderer;
        private Vector3 point0, point1, point2, point3;

        private int numPoints = 50;
        private Vector3[] positions = new Vector3[50];
        private Vector3[] emptyPositions = new Vector3[50];

        //public float height = 2f;

        private bool startDrawingBezierCurve = false;

        [SerializeField] private Transform aimer;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            
        }

        private void Update()
        {
            if (!startDrawingBezierCurve) return;

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(mouseRay, out hit)) return;

            point3 = hit.point;

            SetLineColor(hit);

            aimer.position = point3;

            Vector3 disVec = (point3 - point0);
            point1 = point0 + new Vector3(disVec.x * 0.25f, disVec.y + Mathf.Clamp(40f / disVec.magnitude, 2, 8), disVec.z * 0.25f);
            point2 = point0 + new Vector3(disVec.x * 0.65f, disVec.y + Mathf.Clamp(40f / disVec.magnitude, 2, 8), disVec.z * 0.65f);
            DrawCubicCurve();
        }

        private void SetLineColor(RaycastHit hit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                aimer.GetComponent<SpriteRenderer>().color = Color.green;
                FormationManager.Instance.unitsCanAttack = false;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Student"))
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                aimer.GetComponent<SpriteRenderer>().color = Color.red;
                FormationManager.Instance.unitsCanAttack = true;
            }
        }

        private void DrawCubicCurve()
        {
            for (int i = 1; i < numPoints+1; i++)
            {
                float t = i / (float)numPoints;
                positions[i - 1] = CalculateCubicBezierPoint(t, point0, point1, point2, point3);
            }
            lineRenderer.SetPositions(positions);
        }

        private Vector3 CalculateCubicBezierPoint(float t, Vector3 position1, Vector3 position2, Vector3 position3, Vector3 position4)
        {
            return Mathf.Pow(1 - t, 3) * position1 +
                    3 * Mathf.Pow(1 - t, 2) * t * position2 +
                    3 * (1 - t) * Mathf.Pow(t, 2) * position3 +
                    Mathf.Pow(t, 3) * position4;
        }

        public void SetStartPoint(Vector3 startPointPos)
        {
            point0 = startPointPos;
            startDrawingBezierCurve = true;
            aimer.gameObject.SetActive(true);
        }

        public void StopDrawingCurve()
        {
            startDrawingBezierCurve = false;
            lineRenderer.SetPositions(emptyPositions);
            aimer.gameObject.SetActive(false);
        }
    }
}