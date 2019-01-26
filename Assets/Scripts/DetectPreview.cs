
using UnityEngine;
namespace Battle
{
	public class DetectPreview : MonoBehaviour
	{
		public Color DetectColor = new Color(1, 0, 0, 0.1f);
		//public Color ShootColor = new Color(0, 1, 0, 0.1f);

		//public float DetectAngle = 360;
		public float DetectRange = 5f;
		//public float ShootRangeMax = 5f;

		public bool IsSelectedShow;

		private void Show()
		{
			//float theta = (DetectAngle + 180) / 360f / 2;
			

			//Vector3 v = new Vector3(Mathf.Cos(theta * 2 * Mathf.PI), 0f, Mathf.Sin(theta * 2 * Mathf.PI)).normalized;
#if UNITY_EDITOR
			UnityEditor.Handles.color = DetectColor;
			UnityEditor.Handles.DrawSolidDisc(transform.position, new Vector3(0, 0, 1), DetectRange);// v, DetectAngle, DetectRange);
#endif

		}

		private void OnDrawGizmos()
		{
			if (IsSelectedShow == false)
			{
				Show();
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (IsSelectedShow)
			{
				Show();
			}

		}
	}
}