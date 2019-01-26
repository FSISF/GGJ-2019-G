using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
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
}
