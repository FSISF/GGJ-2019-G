using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFun.Gameplay.BehaviourTree
{
	public class SequeterMono : AINodeMono
	{
		public Sequeter Create()
		{
			Sequeter instance = new Sequeter();
			OnGetInstance(instance);
			return instance;
		}
	}
}
