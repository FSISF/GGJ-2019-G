using System;
using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

[Serializable]
public class GGAIMember : AIMember
{
	public CharInterface Self;
	public float MostCloseEmemyDist = 9999f;

	public override object Get(string key)
	{
		switch (key)
		{
			case "MostCloseEmemyDist":
				return MostCloseEmemyDist;
		}

		return base.Get(key);
	}

	public override void Set(string key, object val)
	{
		//switch (key)
		//{
		//	case "InDefault":
		//		InDefault = (float)val;
		//		return;
		//	case "GridPos":
		//		GridPos = (Vector3)val;
		//		return;
		//	case "EchoIndex":
		//		EchoIndex = (float)val;
		//		return;

		//}

		base.Set(key, val);
	}
}
