namespace JFun.Gameplay.BehaviourTree
{
	public class WhileConditionMono : ConditionMono
	{
		public new WhileCondition Create(AIMember aiMember)
		{
			_aiMember = aiMember;

			WhileCondition instance = new WhileCondition(Comparison);
			OnGetInstance(instance);
			return instance;
		}
	}
}
