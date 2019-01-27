using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class MonsterAtkNodeMono : AINodeMono
{
	public float MoveDuration;
	public int Damage = 1;

	public MonsterAtkNode Create()
	{
		return new MonsterAtkNode(MoveDuration, Damage);
	}
}

public class MonsterAtkNode : AINode
{
	private float _duration;
	private int _damage;

	public MonsterAtkNode(float duration, int damage)
	{
		_duration = duration;
		_damage = damage;
	}

	public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
	{
		GGAIMember member = aiMember as GGAIMember;
		CharInterface player = CharManager.Instance.MainChar;
		member.Self.transform.DOMove(player.transform.position, 0.3f).OnComplete(()=>
		{
			player.TakeDamage(_damage);
		});

		return AINodeResult.Success;
	}
}
