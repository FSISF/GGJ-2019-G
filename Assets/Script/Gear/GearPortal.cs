using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPortal : Gear
{
    public Vector3 portalPos;
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
            PlayerController.Instance.transform.position = portalPos;
            Debug.Log("Portal");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(portalPos, 0.2f);
    }
}
