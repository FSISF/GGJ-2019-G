using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class AITester : MonoBehaviour
	{
		[SerializeField]
		private AIMember _aiMember;
		[SerializeField]
		private AIController _aiController;

		private void Awake()
		{
			// 可以創建任何 有繼承 AIMemeber的Class 塞進去

			AGAIMember agM = new AGAIMember();

			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.name = "[IM AG TEST CUBE]";
			cube.transform.position = Vector3.zero;
			agM.TestGameObject = cube;
			_aiMember = agM;

			RootNode root = AITreeHelper.Parser(transform, _aiMember);
			_aiController.Init(_aiMember, root);
			_aiController.StartAI();
		}
	}
}
