using System.Collections;
using System.Collections.Generic;
using JFun.Gameplay.BehaviourTree;
using UnityEngine;

public class MonsterAtkNodeMono : AINodeMono
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MonsterAtkNode : AINode
{
	public override AINodeResult Execute(ref AINode lastRunningNode, AIMember aiMember)
	{


		return AINodeResult.Success;
	}
}
