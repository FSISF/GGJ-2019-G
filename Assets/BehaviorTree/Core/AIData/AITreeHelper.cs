using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JFun.Gameplay.BehaviourTree
{
	public static class AITreeHelper
	{
		public static RootNode Parser(Transform t, AIMember aiMember)
		{
			RootNode result = new RootNode();
			SearchAndSetChild(t, result, aiMember);
			return result;
		}

		private static void SearchAndSetChild(Transform t, AINodeContainer composite, AIMember aiMember)
		{
			foreach (Transform child in t)
			{
				AINode node = null;

				SelectMono selectMono = child.GetComponent<SelectMono>();
				if (selectMono != null)
				{
					node = selectMono.Create();
				}

				SequeterMono sequterMono = child.GetComponent<SequeterMono>();
				if (sequterMono != null)
				{
					node = sequterMono.Create();
				}

				ConditionMono conditionMono = child.GetComponent<ConditionMono>();
				if (conditionMono != null)
				{
					if (conditionMono is WhileConditionMono)
					{
						WhileConditionMono wCondition = conditionMono as WhileConditionMono;
						node = wCondition.Create(aiMember);
					}
					else
					{
						node = conditionMono.Create(aiMember);
					}
				}

				PrintActionMono printMono = child.GetComponent<PrintActionMono>();
				if (printMono != null)
				{
					node = printMono.Create();
				}

				DelayActionMono delayMono = child.GetComponent<DelayActionMono>();
				if (delayMono != null)
				{
					node = delayMono.Create();
				}

				SetValActionMono setValMono = child.GetComponent<SetValActionMono>();
				if (setValMono != null)
				{
					node = setValMono.Create(aiMember);
				}

		#region AGAction
				CustomAiNodeMono customMono = child.GetComponent<CustomAiNodeMono>();
				if (customMono != null)
				{
					node = customMono.Create();
				}
				MonsterSwitchNavNodeMono monsterNavMono = child.GetComponent<MonsterSwitchNavNodeMono>();
				if (monsterNavMono != null)
				{
					node = monsterNavMono.Create();
				}
		#endregion
				composite.AddChild(node);
				if (node is AINodeContainer)
				{
					AINodeContainer nodeC = node as AINodeContainer;
					SearchAndSetChild(child.transform, nodeC, aiMember);
				}
			}
		}
	}
}
