using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class SetValActionMono : AINodeMono
	{
		public enum SetValType
		{
			Assign,
			Add,
			Sub
		}

		[SerializeField]
		private SetValType _setValType = SetValType.Assign;
		[SerializeField]
		private string _fieldName;
		[SerializeField]
		private float _val;
		private AIMember _aiMember;

		public SetValAction Create(AIMember aiMember)
		{
			_aiMember = aiMember;



			SetValAction instance = null;

			if (_setValType == SetValType.Assign)
			{
				instance = new SetValAction(AssignVal);
			}
			else if (_setValType == SetValType.Add)
			{
				instance = new SetValAction(AddVal);
			}
			else if (_setValType == SetValType.Sub)
			{
				instance = new SetValAction(SubVal);
			}

			OnGetInstance(instance);
			return instance;
		}

		public void AssignVal()
		{
			var fieldInfo = _aiMember.GetType().GetField(_fieldName);

			if (fieldInfo == null)
			{
				Debug.LogErrorFormat("[SetValActionMono][AssignVal] NodeName {0} , _fieldName {1} Not Found In Member"
					, gameObject.name, _fieldName);
				return;
			}

			_aiMember.GetType().GetField(_fieldName).SetValue(_aiMember, _val);
		}

		public void AddVal()
		{
			float val = (float)_aiMember.GetType().GetField(_fieldName).GetValue(_aiMember);
			_aiMember.GetType().GetField(_fieldName).SetValue(_aiMember, val + _val);
		}

		public void SubVal()
		{
			float val = (float)_aiMember.GetType().GetField(_fieldName).GetValue(_aiMember);
			_aiMember.GetType().GetField(_fieldName).SetValue(_aiMember, val - _val);
		}
	}
}
