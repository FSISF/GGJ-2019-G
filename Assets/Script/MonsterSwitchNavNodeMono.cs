using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class MonsterSwitchNavNodeMono : AINodeMono
{
	[SerializeField]
	private NavType _nav;

	public MonsterSwitchNavNode Create()
	{
		MonsterSwitchNavNode instance = new MonsterSwitchNavNode(_nav);
		OnGetInstance(instance);
		return instance;
	}
}
