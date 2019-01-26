using System;
using System.Collections.Generic;
using JFun.Gameplay.Utility;
using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public enum AINodeResult
	{
		None,
		Running,
		Success,
		Failure
	}

	public class AIController : MonoBehaviour
	{
		[SerializeField]
		private AIMember _aiMember;
		private ITask _updateTask = null;
		private AINode _lastRunningNode = null;
		private RootNode _rootNode;
		private List<AINode> AllNodes = new List<AINode>();

		public event Action OnUpdateBefore = delegate { };

		[SerializeField]
		public bool RunTestCase;

		private float _timer
		{
			get
			{
				return _aiMember.Timer;
			}
			set
			{
				_aiMember.Timer = value;
			}
		}
		private float _tickTime = 0.5f;

		public void Init(AIMember aiMember, RootNode rootNode)
		{
			AllNodes.Add(rootNode);
			SearchChilds(rootNode);
			_aiMember = aiMember;
			_rootNode = rootNode;
		}

		public void StartAI()
		{
			if (_aiMember == null)
			{
				Debug.LogErrorFormat("[CharAISystem][AIController][StartAI] _aiMember == null");
				return;
			}

			if (_rootNode == null)
			{
				Debug.LogErrorFormat("[CharAISystem][AIController][StartAI] _rootNode == null");
				return;
			}

			_updateTask = ScheduleHelper.Instance.NewScheuleRepeat(UpdateAI, _tickTime);
		}

		public void StopAI()
		{
			_updateTask.Dispose();
		}

		public void Pause()
		{
			_updateTask.Pause();
		}

		public void Resume()
		{
			_updateTask.Resume();
		}

		public void Release()
		{
			Debug.LogFormat("[AIController][Release]");

			_updateTask.Dispose();
			_updateTask = null;

			foreach (AINode n in AllNodes)
			{
				n.Release();
			}
		}

		private void UpdateAI()
		{
			OnUpdateBefore();
			CleanNodesState();

			_aiMember.Timer += _tickTime;
			if (_lastRunningNode == null)
			{
				_rootNode.Run(ref _lastRunningNode, _aiMember);
			}
			else
			{
				_lastRunningNode.Parent.Run(ref _lastRunningNode, _aiMember);
			}
		}

		private RootNode TestCase(AIMember aiMember)
		{
			RootNode root = new RootNode();
			root
				.AddChild(new Selector()
					.AddChild(new Condition(() => _timer > 3 && aiMember.Index < 1)
						.AddChild(new Sequeter()
							.AddChild(new PrintAction("[1]State In"))
							.AddChild(new SetValAction(() => aiMember.Index = 1))
							.AddChild(new DelayAction(5f))
							.AddChild(new PrintAction("[1]State Out"))))
					.AddChild(new Condition(() => _timer > 10 && aiMember.Index < 2)
						.AddChild(new Sequeter()
							.AddChild(new PrintAction("[2]State In"))
							.AddChild(new SetValAction(() => aiMember.Index = 2))
							.AddChild(new DelayAction(5f))
							.AddChild(new PrintAction("[2]State Out"))))
					.AddChild(new Condition(() => _timer > 20f)
							.AddChild(new Sequeter()
								.AddChild(new SetValAction(() => _timer = 0))
								.AddChild(new SetValAction(() => aiMember.Index = 0))))
					.AddChild(new PrintAction("[3] Default Case")));

			return root;
		}

		private void Start()
		{
			if (RunTestCase)
			{
				AIMember aiMember = new AGAIMember();
				RootNode root = TestCase(aiMember);
				Init(aiMember, root);
				StartAI();
			}
		}

		private void CleanNodesState()
		{
			foreach (AINode node in AllNodes)
			{
				node.ResultState = AINodeResult.None;
			}
		}

		private void SearchChilds(AINodeContainer container)
		{
			foreach (AINode node in container.Childs)
			{
				AllNodes.Add(node);
				if (node is AINodeContainer)
				{
					AINodeContainer c = node as AINodeContainer;
					SearchChilds(c);
				}
			}
		}
	}

	public abstract class AINode
	{
		public event Action<AINodeResult> OnResult = delegate { };

		public AINodeResult ResultState
		{
			set
			{
				OnResult(value);
				_resultState = value;
			}
			get
			{
				return _resultState;
			}
		}

		private AINodeResult _resultState;

		public AINode Parent
		{
			get; set;
		}
		public AINodeResult Run(ref AINode lastRunningNode, AIMember aiMember)
		{
			AINodeResult result = Execute(ref lastRunningNode, aiMember);
			ResultState = result;
			return result;
		}

		public abstract AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember);

		public virtual void Release()
		{
		}
	}

	public abstract class AINodeContainer : AINode
	{
		public List<AINode> Childs
		{
			get
			{
				return _childs;
			}
		}

		private List<AINode> _childs = new List<AINode>();

		public virtual AINodeContainer AddChild(AINode node)
		{
			node.Parent = this;
			_childs.Add(node);
			return this;
		}
	}

	[Serializable]
	public class RootNode : Sequeter
	{
	}
	public sealed class WhileCondition : AINodeContainer
	{
		public AINode TrueNode
		{
			get
			{
				return Childs[0];
			}
		}
		private bool _trueNodeInRunning;

		private Func<bool> _conditionFunc;
		public WhileCondition(Func<bool> conditionFunc)
		{
			_conditionFunc = conditionFunc;
		}

		public override AINodeContainer AddChild(AINode node)
		{
			if (Childs.Count > 0)
			{
				Debug.LogErrorFormat("[WhileCondition][AddChild] Have it already. ");
			}

			return base.AddChild(node);
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			if (_trueNodeInRunning)
			{
				_trueNodeInRunning = false;
				return RunTrueNode(ref lastRunningNode, aiMember);
			}

			if (_conditionFunc() == false)
			{
				return AINodeResult.Running;
			}

			if (TrueNode == null)
			{
				return AINodeResult.Success;
			}
			else
			{
				return RunTrueNode(ref lastRunningNode, aiMember);
			}
		}

		private AINodeResult RunTrueNode(ref AINode lastRunningNode, AIMember aiMember)
		{
			AINodeResult nResult = TrueNode.Run(ref lastRunningNode, aiMember);
			if (nResult == AINodeResult.Running)
			{
				_trueNodeInRunning = true;
				lastRunningNode = TrueNode;
				return AINodeResult.Running;
			}

			return nResult;
		}
	}

	[Serializable]
	public sealed class Condition : AINodeContainer
	{
		public AINode TrueNode
		{
			get
			{
				if (Childs.Count == 0)
					return null;

				return Childs[0];
			}
		}
		private bool _trueNodeInRunning;

		private Func<bool> _conditionFunc;
		public Condition(Func<bool> conditionFunc)
		{
			_conditionFunc = conditionFunc;
		}

		public override AINodeContainer AddChild(AINode node)
		{
			if (Childs.Count > 0)
			{
				Debug.LogErrorFormat("[Condition][AddChild] Have it already. ");
			}

			return base.AddChild(node);
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			if (_trueNodeInRunning)
			{
				_trueNodeInRunning = false;
				return RunTrueNode(ref lastRunningNode, aiMember);
			}

			if (_conditionFunc() == false)
			{
				return AINodeResult.Failure;
			}

			if (TrueNode == null)
			{
				return AINodeResult.Success;
			}
			else
			{
				return RunTrueNode(ref lastRunningNode, aiMember);
			}
		}

		private AINodeResult RunTrueNode(ref AINode lastRunningNode, AIMember aiMember)
		{
			AINodeResult nResult = TrueNode.Run(ref lastRunningNode, aiMember);
			if (nResult == AINodeResult.Running)
			{
				_trueNodeInRunning = true;
				lastRunningNode = TrueNode;
				return AINodeResult.Running;
			}

			return nResult;
		}
	}

	[Serializable]
	public class Sequeter : AINodeContainer
	{
		private int index = 0;
		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			lastRunningNode = null;
			for (; index < Childs.Count; index++)
			{
				AINode node = Childs[index];
				AINodeResult nResult = node.Run(ref lastRunningNode, aiMember);

				if (nResult == AINodeResult.Running)
				{
					lastRunningNode = node;
					return AINodeResult.Running;
				}

				if (nResult == AINodeResult.Failure)
				{
					index = 0;
					return AINodeResult.Failure;
				}
			}

			lastRunningNode = null;
			index = 0;
			return AINodeResult.Success;
		}
	}

	[Serializable]
	public class Selector : AINodeContainer
	{
		private int index = 0;
		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			lastRunningNode = null;
			for (; index < Childs.Count; index++)
			{
				AINode node = Childs[index];
				AINodeResult nResult = node.Run(ref lastRunningNode, aiMember);

				if (nResult == AINodeResult.Failure)
				{
					continue;
				}

				if (nResult == AINodeResult.Success)
				{
					index = 0;
					return AINodeResult.Success;
				}

				if (nResult == AINodeResult.Running)
				{
					lastRunningNode = node;
					return AINodeResult.Running;
				}
			}

			index = 0;
			return AINodeResult.Failure;
		}
	}

	[Serializable]
	public class AIMember
	{
		public float Timer;
		public float Index;

		public virtual System.Object Get(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				Debug.LogErrorFormat("[AIMember][Get] Key IsNullorEmpty");
				return null;
			}

			switch (key)
			{
				case "Timer":
					return Timer;
				case "Index":
					return Index;
				default:
					Debug.LogErrorFormat("[AIMember][Get]Not Found This Key {0}", key);
					return null;
			}
		}

		public virtual void Set(string key, System.Object val)
		{
			switch (key)
			{
				case "Timer":
					Timer = (float)val;
					break;
				case "Index":
					Index = (float)val;
					break;
				default:
					Debug.LogErrorFormat("[AIMember][Get]Not Found This Key {0}", key);
					break;
			}
		}
	}

	public class FailureAction : AINode
	{
		private string _print;
		public FailureAction(string print = "")
		{
			_print = print;
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			if (!string.IsNullOrEmpty(_print))
			{
				Debug.LogFormat("[PrintAction] {0}", _print);
			}
			return AINodeResult.Failure;
		}
	}

	public class SetValAction : AINode
	{
		private Action _setValAction;
		public SetValAction(Action setValAction)
		{
			_setValAction = setValAction;
		}

		public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
		{
			_setValAction();
			return AINodeResult.Success;
		}
	}
}
