using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class DelayActionMono : AINodeMono
	{
		[SerializeField]
		private float durtaion;

		public DelayAction Create()
		{
			DelayAction instance = new DelayAction(durtaion);
			OnGetInstance(instance);
			return instance;
		}
	}
}
