using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour
{
	public int LiveMonsterCount;
	public int TotalMosterCount;

	public event Action OnMonsterCountChange = delegate { };

	public List<CharInterface> _charList;

	public static CharManager Instance;

	public CharInterface MainChar;

	public void Register(CharInterface charInterface)
	{
		_charList.Add(charInterface);
		if (charInterface.Team == Team.Monster)
		{
			charInterface.OnHpZero += () =>
			{
				LiveMonsterCount -= 1;
				OnMonsterCountChange.Invoke();
			};
			LiveMonsterCount++;
			TotalMosterCount++;
		}
	}

	public void UnRegister(CharInterface charInterface)
	{
		_charList.Remove(charInterface);
	}

	private void Awake()
	{
		Instance = this;
	}
}
