
//namespace JFun.Gameplay.AG.Battle
//{
//	public class DetectPreview : MonoBehavor
//		private void OnDrawGizmosSelected()
//		{
//			if (Gun == null)
//				return;

//			if (Gun.Content == ShootContent.Bullet)
//			{
//				float theta = (DetectAngle + 180) / 360f / 2;

//				if (DirectionData != null)
//				{
//					switch (DirectionData.Direction)
//					{
//						case Direction.Up:
//							theta = (DetectAngle + 180) / 360f / 2;
//							break;
//						case Direction.Left:
//							theta = (DetectAngle - 360) / 360f / 2;
//							break;
//						case Direction.Right:
//							theta = (DetectAngle) / 360f / 2;
//							break;
//						case Direction.Down:
//							theta = (DetectAngle - 180) / 360f / 2;
//							break;

//						default:
//							break;
//					}
//				}

//				Vector3 v = new Vector3(Mathf.Cos(theta * 2 * Mathf.PI), 0f, Mathf.Sin(theta * 2 * Mathf.PI)).normalized;
//#if UNITY_EDITOR
//				UnityEditor.Handles.color = DetectColor;
//				UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, v, DetectAngle, DetectRange);

//				UnityEditor.Handles.color = ShootColor;
//				UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, v, DetectAngle, Gun.ShootRangeMax);

//				if (Target != null)
//				{
//					UnityEditor.Handles.color = Color.red;
//					UnityEditor.Handles.DrawLine(transform.position, Target.position);
//				}
//#endif
//			}
//		}
//	}
//}