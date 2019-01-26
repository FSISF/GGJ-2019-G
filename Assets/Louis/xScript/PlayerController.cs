using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PlayMaker;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2;
    public Rigidbody2D rb;
    public Vector2 MoveVelocity;
    public CharacterState state;
    public Transform FollowPoint;
    public float AttackCD = 0.5f;
    public bool CanBite;
    bool biting;
    public PlayMakerFSM AttackFSM;
    Tweener BiteTween;
    Collision2D lastCollision;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Speed = state.WalkSpeed;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MoveVelocity = moveInput.normalized * Speed;
    }
    void FixedUpdate()
    {
        //
        if (Input.GetMouseButtonDown(0) && CanBite)
        {
            AttackFSM.SendEvent("Bite");
            BiteTween = rb.DOMove(FollowPoint.position, 0.2f).OnPlay(BiteStart).OnComplete(BiteEnd);
        }
        else
        {

        }
        if (!biting)
            rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //BiteTween.Kill();
        //lastCollision = collision;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    void BiteStart()
    {
        biting = true;
    }
    void BiteEnd()
    {
        biting = false;
    }
}
