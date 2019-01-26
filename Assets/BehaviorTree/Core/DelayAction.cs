namespace JFun.Gameplay.BehaviourTree
{
	public class DelayAction : AINode
	{
		private float _duration;
		private bool _inRunning;
		private float _endTime;

		public DelayAction(float duration)
		{
			_duration = duration;
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			if (_inRunning == true)
			{
				if (aiMember.Timer > _endTime)
				{
					_inRunning = false;
					return AINodeResult.Success;
				}
				else
				{
					return AINodeResult.Running;
				}
			}
			else
			{
				_inRunning = true;
				_endTime = aiMember.Timer + _duration;
				return AINodeResult.Running;
			}
		}
	}
}
