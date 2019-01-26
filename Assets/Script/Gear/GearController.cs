using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    public bool DebugTest = false;

    Gear gear = null;
    GearTrigger gearTrigger = null;

    public bool IsActive()
    {
        return gear.IsActive;
    }

    void Awake()
    {
        if(gear == null)
        {
            gear = GetComponentInChildren<Gear>();
        }
        if(gearTrigger == null)
        {
            gearTrigger = GetComponentInChildren<GearTrigger>();
        }
        gear.Init();
        gearTrigger.Init();
    }

    private void Update()
    {
        if (DebugTest)
        {
            DebugTest = false;
            gear.Turn(true);
        }
    }

    public void Turn(bool turnOn)
    {
        gear.Turn(turnOn);
    }
}
