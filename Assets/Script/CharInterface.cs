using System;
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
	public int HpMax = 10;
	public int Hp = 10;
	public event Action OnHpZero = delegate { };
    public event Action OnHPChange = delegate { };
	public bool IsDead = false;

	[SerializeField]
	private MonsterNav _monsterNav;
	public Team Team;
	CharacterState charState;

	public void TakeDamage(int damage)
	{
		if (IsDead)
			return;

		GetComponent<Animator>().SetTrigger("Hit");
		GetComponent<CharacterState>().Hit();

		Hp -= damage;
        OnHPChange.Invoke();

        if (Hp <= 0)
		{
			Hp = 0;
			IsDead = true;
			GetComponent<CharacterState>().OnDeathTrigger();
			OnHpZero.Invoke();

			if (CharManager.Instance == null)
			{
				Debug.LogFormat("[CharInterface.Start] Not Found CharManager.Instance");
				return;
			}

			CharManager.Instance.UnRegister(this);
			//TODO Death Show
		}
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

	private void Start()
	{
		Hp = HpMax;
		if (CharManager.Instance == null)
		{
			Debug.LogFormat("[CharInterface.Start] Not Found CharManager.Instance");
			return;
		}
		CharManager.Instance.Register(this);
	}

	private void Awake()
	{
		charState = GetComponent<CharacterState>();
	}
}
