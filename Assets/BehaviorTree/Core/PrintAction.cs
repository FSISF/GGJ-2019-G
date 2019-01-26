using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class PrintAction : AINode
	{
		private string _print;
		public PrintAction(string print)
		{
			_print = print;
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			Debug.LogFormat("[PrintAction] {0}", _print);
			return AINodeResult.Success;
		}
	}
}