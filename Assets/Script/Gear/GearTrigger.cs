using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearTrigger : MonoBehaviour
{
    public bool IsActive = false;
    private GearController gearController = null;
    public virtual void Init()
    {
        IsActive = false;
        if (gearController == null)
        {
            gearController = transform.parent.GetComponent<GearController>();
        }
    }
    public virtual void Turn(bool turnOn)
    {
        if(IsActive == turnOn)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            gearController.Turn(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            gearController.Turn(false);
        }
    }
}
