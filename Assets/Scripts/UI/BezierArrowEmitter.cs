using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieArmy.UI
{
	/// <summary>
	/// 
	/// </summary>
	public class BezierArrowEmitter : MonoBehaviour
	{
		[SerializeField] private GameObject arrowHeadPrefab;
		[SerializeField] private GameObject arrowNodePrefab;
		[SerializeField] private int arrowNodeNum;	
		[SerializeField] private float scaleFactor;

		//private RectTransform origin;
		public Transform origin;
		private List<Transform> arrowNodes = new List<Transform>();
		private List<Vector2> controlPoints = new List<Vector2>();
		private readonly List<Vector2> controlPointFactors = new List<Vector2>() { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };

        private void Awake()
        {
			//origin = GetComponent<RectTransform>();

            for (int i = 0; i < arrowNodeNum; i++)
            {
				arrowNodes.Add(Instantiate(arrowNodePrefab, transform).transform);
            }

			arrowNodes.Add(Instantiate(arrowHeadPrefab, transform).transform);

			arrowNodes.ForEach(n => n.transform.position = new Vector2(-1000, -1000));

            for (int i = 0; i < 4; i++)
            {
				controlPoints.Add(Vector2.zero);
            }
		}
        private void Update()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(mouseRay, out hit)) return;
            //Vector3 hitPoint = origin.position + new Vector3(10, 10* Mathf.Sqrt(2), 10);

            //先忽略z 计算x和y 在xy平面上计算曲线 再将曲线平面绕y轴旋转到对应方向

            Vector3 relativePostion = hit.point - origin.position;


            float alpha = Vector3.Angle(Vector3.up, relativePostion);
            Vector3 crossVec = Vector3.Cross(relativePostion, Vector3.up);
            float beta = Vector3.Angle(crossVec, Vector3.forward);
            //Vector2 relativeVec = new Vector2(relativePostion.magnitude * Mathf.Sin(alpha * Mathf.Deg2Rad), relativePostion.magnitude * Mathf.Cos(alpha * Mathf.Deg2Rad));

            //controlPoints[0] = new Vector2(origin.position.x, origin.position.y);
            controlPoints[0] = origin.position;

			//controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			controlPoints[3] = hit.point;

			//(0,0) (-0.3, 0) (0.1, 0)  (1,0)

			//controlPointFactors[0] = 

			//P1 = P0 +	(P3 - P0) * Vector2(-0.3f, 0.8f)
			//P2 = P0 +	(P3 - P0) * Vector2(0.1f, 1.4f)
			controlPoints[1] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[0];
			controlPoints[2] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[1];


            for (int i = 0; i < arrowNodes.Count; i++)
            {
				//计算t
				float t = Mathf.Log(1f * i / (arrowNodes.Count - 1) + 1f, 2f);

				//根据Cubic Bezier curve 公式计算位置
				// B(t) = (1-t)^3 * P0 + 3 * (1-t)^2 * t * P1 + 3 * (1-t) * t^2 * P2 + t^3 * P3

				Vector2 relativeBezierPosXYPlane = CalculateCubicBezierCurvePointPos(controlPoints, t);

				float angle = Vector3.Angle(Vector2.right, relativeBezierPosXYPlane);
				Vector3 rotatedRelativePosition = new Vector3(relativePostion.magnitude * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Cos(beta*Mathf.Deg2Rad), relativePostion.magnitude * Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Cos(beta * Mathf.Deg2Rad), relativePostion.magnitude * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Sin(beta * Mathf.Deg2Rad));
				arrowNodes[i].position = rotatedRelativePosition + origin.position;

    //            //计算旋转
    //            if (i > 0)
    //            {
				//	Vector3 euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, arrowNodes[i].position - arrowNodes[i - 1].position));
				//	arrowNodes[i].rotation = Quaternion.Euler(euler);
    //            }

				////计算缩放
				//float scale = scaleFactor * (1f - 0.03f * (arrowNodes.Count - 1 - i));
				//arrowNodes[i].localScale = new Vector3(scale, scale, 1);
            }

			//arrowNodes[0].rotation = arrowNodes[1].rotation;
		}

		private Vector2 CalculateCubicBezierCurvePointPos(List<Vector2> controlPoints, float t)
        {
			return  Mathf.Pow(1 - t, 3) * controlPoints[0] +
					3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
					3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
					Mathf.Pow(t, 3) * controlPoints[3];
		}
	}
}