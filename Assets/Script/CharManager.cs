using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour
{
	public List<CharInterface> _charList;

	public static CharManager Instance;

	public CharInterface MainChar;

	public void Register(CharInterface charInterface)
	{
		_charList.Add(charInterface);
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
