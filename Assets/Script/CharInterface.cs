using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
	Monster,
	Hero,
}

public class CharInterface : MonoBehaviour
{

	[SerializeField]
	private MonsterNav _monsterNav;
	public Team Team;

	public void TakeDamage(int damage)
	{
		//TODO
	}

	public void ChangeNav(NavType nav)
	{
		if (_monsterNav != null)
		{
			_monsterNav.SwitchNav(nav);
		}
		else
		{
			Debug.LogError("Not Found ");
		}
	}

	public void Shoot(Bullet b , CharInterface target)
	{
		if (target == null)
		{
			Debug.LogError("[CharInterface][Shoot] target == null");
		}

		//TODO Clone Bullet & Shoot
	}

	private void Start()
	{
		CharManager.Instance.Register(this);
	}

	private void OnDestroy()
	{
		CharManager.Instance.UnRegister(this);
	}
}
