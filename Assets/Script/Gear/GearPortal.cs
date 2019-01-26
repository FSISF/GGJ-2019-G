using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPortal : Gear
{
    public Vector2 portalPos;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Init()
    {
        base.Init();
    }
    public override void Turn(bool turnOn)
    {
        base.Turn(turnOn);
        if (turnOn)
        {
            
        }
    }
}
