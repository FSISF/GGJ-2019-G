using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class CustomAiNodeMono : AINodeMono
{
	[SerializeField]
	private Vector3 _move;

	public CustomAINode Create()
	{
		CustomAINode instance = new CustomAINode(_move);
		OnGetInstance(instance);
		return instance;
	}
}
