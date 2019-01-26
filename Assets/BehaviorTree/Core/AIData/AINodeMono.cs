using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public abstract class AINodeMono : MonoBehaviour
	{
		public AINodeResult ResultState = AINodeResult.None;
		/// <summary>
		/// Child Need Call
		/// </summary>
		/// <param name="aiNode"></param>
		public void OnGetInstance(AINode aiNode)
		{
			aiNode.OnResult += OnResult;
		}

		private void OnResult(AINodeResult result)
		{
			ResultState = result;
		}
	}
}
