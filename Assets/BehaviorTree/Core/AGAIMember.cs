using System;
using UnityEngine;
namespace JFun.Gameplay.BehaviourTree
{
	[Serializable]
	public class AGAIMember : AIMember
	{
		public Vector3 GridPos;
		public float EchoIndex;
		public float InDefault;
		public GameObject TestGameObject;

		public override object Get(string key)
		{
			switch (key)
			{
				case "InDefault":
					return InDefault;
				case "GridPos":
					return GridPos;
				case "EchoIndex":
					return EchoIndex;
					break;
			}

			return base.Get(key);
		}

		public override void Set(string key, object val)
		{
			switch (key)
			{
				case "InDefault":
					InDefault = (float)val;
					return;
				case "GridPos":
					GridPos = (Vector3)val;
					return;
				case "EchoIndex":
					EchoIndex = (float)val;
					return;

			}

			base.Set(key, val);
		}
	}
}