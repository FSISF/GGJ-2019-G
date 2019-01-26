using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class ShootBulletNodeMono : AINodeMono
{
	[SerializeField]
	private Bullet _bullet;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _range;

	public ShootBulletNode Create()
	{
		ShootBulletNode instance = new ShootBulletNode(_bullet, _speed, _range);
		OnGetInstance(instance);
		return instance;
	}
}

public class ShootBulletNode : AINode
{
	Bullet _b;
	float _speed;
	float _range;

	public ShootBulletNode(Bullet b, float speed, float range)
	{
		_b = b;
		_speed = speed;
		_range = range;
	}

	public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
	{
		GGAIMember member = aiMember as GGAIMember;

		if (CharManager.Instance == null)
			return AINodeResult.Failure;

		if (CharManager.Instance.MainChar == null)
			return AINodeResult.Failure;

		Vector3 playerPos = CharManager.Instance.MainChar.transform.position;
		Vector3 myPos = member.Self.transform.position;
		Vector3 dir = playerPos - member.Self.transform.position;

		Bullet b = Object.Instantiate(_b);
		b.transform.position = myPos;

		Vector3 target = playerPos + dir.normalized;
		b.transform.DOMove(target, _range / _speed);

		return AINodeResult.Success;
	}
}
