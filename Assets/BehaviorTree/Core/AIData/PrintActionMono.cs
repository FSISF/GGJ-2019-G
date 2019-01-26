using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class PrintActionMono : AINodeMono
	{
		[SerializeField]
		private string _msg;

		public PrintAction Create()
		{
			PrintAction instance = new PrintAction(_msg);
			OnGetInstance(instance);
			return instance;
		}
	}
}
