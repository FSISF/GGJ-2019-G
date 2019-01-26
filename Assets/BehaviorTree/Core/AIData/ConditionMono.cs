using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JFun.Gameplay.BehaviourTree
{
	public enum ConditionType
	{
		Equal,
		GreaterOrEequal,
		LessOrEequal,
		Greater,
		Less,
		NotEqual,

		Vector3Range = 100
	}

	public enum FieldType
	{
		Float,
		Vector3
	}

	public class ConditionMono : AINodeMono
	{
		[SerializeField]
		protected FieldType _fieledType = FieldType.Float;
		[SerializeField]
		protected string _fieldName;
		[SerializeField]
		protected ConditionType _conditionType;
		[SerializeField]
		protected float _val;
		protected AIMember _aiMember;

		[Header("[↓][Vector3]")]
		[SerializeField]
		protected Vector3 _vec3Val;
		[SerializeField]
		protected float _vec3range = 3;

		public Condition Create(AIMember aiMember)
		{
			_aiMember = aiMember;

			Condition instance = new Condition(Comparison);
			OnGetInstance(instance);
			return instance;
		}

		protected bool Comparison()
		{
			if (_fieledType == FieldType.Float)
			{
				float fieldVal = (float)_aiMember.Get(_fieldName);

				if (_conditionType == ConditionType.Equal)
					return fieldVal == _val;
				else if (_conditionType == ConditionType.Greater)
					return fieldVal > _val;
				if (_conditionType == ConditionType.GreaterOrEequal)
					return fieldVal >= _val;
				else if (_conditionType == ConditionType.Less)
					return fieldVal < _val;
				if (_conditionType == ConditionType.LessOrEequal)
					return fieldVal <= _val;
				else if (_conditionType == ConditionType.NotEqual)
					return fieldVal != _val;
			}

			if (_fieledType == FieldType.Vector3)
			{
				Vector3 fieldVal = (Vector3)_aiMember.Get(_fieldName);

				if (_conditionType == ConditionType.Equal)
					return fieldVal == _vec3Val;
				else if (_conditionType == ConditionType.Vector3Range)
				{
					float dis = Vector3.Distance(fieldVal, _vec3Val);
					bool inRange = dis < _vec3range;

					return inRange;
				}
				else
				{
					Debug.LogErrorFormat("[ConditionMono][Comparison] Filed Type is Vector3 , Not support ConditionType {0}"
						, _conditionType);
				}
			}

			return false;
		}
	}
}
