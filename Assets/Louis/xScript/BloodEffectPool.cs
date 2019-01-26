using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectPool : MonoBehaviour
{
    public Animator[] Pool;
    public static BloodEffectPool Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallBloodEffect(Vector2 WorldPosition)
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            Animator animator = Pool[i];
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
           
            if (!info.IsName("Idle"))
                continue;
            else
            {
                Pool[i].SetTrigger("Show");
                Pool[i].transform.position = WorldPosition;
                return;
            }
        }

        Debug.Log("If you see this message, plz keep copy your bloodeffect obj. You need more.");
    }

}
