using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class CustomAINode : AINode
{
	private Vector3 _move;
	public CustomAINode(Vector3 move)
	{
		_move = move;
	}

	public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
	{
		AGAIMember agMember = aiMember as AGAIMember;
		agMember.TestGameObject.transform.position += _move;
		return AINodeResult.Success;
	}
}
