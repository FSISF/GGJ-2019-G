using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterState : MonoBehaviour
{
    public float HealthPoint = 100;
    public float AttackPoint = 20;
    public float WalkSpeed = 5;
    public float RunSpeed = 10;

    [Header("Events")]
    public UnityEvent OnHurt;
    public UnityEvent OnDeath;
    public UnityEvent OnWalkStart;
    public UnityEvent OnWalking;
    public UnityEvent OnRunStart;
    public UnityEvent OnRuning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {
        BloodEffectPool.Instance.CallBloodEffect(transform.position);
        OnHurt.Invoke();
    }
}
