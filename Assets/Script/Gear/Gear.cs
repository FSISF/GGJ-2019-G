using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool IsActive = false;
    public virtual void Init()
    {
        IsActive = false;
    }
    public virtual void Turn(bool turnOn)
    {
        if (IsActive == turnOn)
        {
            return;
        }
        IsActive = turnOn;
    }

    public virtual void Awake()
    {

    }
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
}
