using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class MonsterSwitchNavNode : AINode
{
	private NavType _nav;

	public MonsterSwitchNavNode(NavType nav)
	{
		_nav = nav;
	}

	public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
	{
		GGAIMember member = aiMember as GGAIMember;
		member.Self.ChangeNav(_nav);
		return AINodeResult.Success;
	}
}
