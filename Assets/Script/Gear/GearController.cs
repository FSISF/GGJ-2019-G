using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    List<Gear> gears = null;
    GearTrigger gearTrigger = null;
    bool isActive = false;
    public bool TestSwitchOn = false;
    public bool IsActive()
    {
        return isActive;
    }

    void Awake()
    {
        if(gears == null)
        {
            gears = new List<Gear>();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Gear gear = transform.GetChild(i).GetComponent<Gear>();
            if (gear != null && !gears.Contains(gear))
            {
                gears.Add(gear);
            }
        }

        if (gearTrigger == null)
        { 
            gearTrigger = GetComponentInChildren<GearTrigger>();
        }
        isActive = false;
        for(int i = 0; i < gears.Count; i++)
        {
            gears[i].Init();
        }
        gearTrigger.Init();
    }

    public void Turn(bool isActive)
    {
        for(int i = 0; i < gears.Count; i++)
        {
            gears[i].Turn(isActive);
        }
    }

    private void Update()
    {
        if (TestSwitchOn)
        {
            TestSwitchOn = false;
            Turn(true);
        }
    }
}
