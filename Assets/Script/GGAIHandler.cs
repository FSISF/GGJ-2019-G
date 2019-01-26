using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public class GGAIHandler : MonoBehaviour
	{
		[SerializeField]
		private GGAIMember _aiMember = new GGAIMember();
		[SerializeField]
		private AIController _aiController;

		private void Update()
		{
			//Update MostCloseEmemyDist
			Vector3 myPos = _aiMember.Self.transform.position;
			_aiMember.MostCloseEmemyDist = 9999f;
			foreach (CharInterface c in CharManager.Instance._charList)
			{
				bool NotMyTeam = c.Team != _aiMember.Self.Team;
				if (NotMyTeam)
				{
					
					_aiMember.MostCloseEmemyDist = Vector3.Distance(c.transform.position, myPos);
				}
			}
		}

		private void Awake()
		{
			RootNode root = AITreeHelper.Parser(transform, _aiMember);
			_aiController.Init(_aiMember, root);
			_aiController.StartAI();
		}
	}
}

