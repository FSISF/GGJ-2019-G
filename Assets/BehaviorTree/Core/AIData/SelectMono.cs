using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class SelectMono	: AINodeMono
	{
		public Selector Create()
		{
			Selector instance = new Selector();
			OnGetInstance(instance);
			return instance;
		}
	}
}
