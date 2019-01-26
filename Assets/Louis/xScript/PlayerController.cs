﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PlayMaker;

public class PlayerController : SingletonMono<PlayerController>
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
    public Animator animator;
    public enum Facing { Up, Down, Left, Right }
    public Facing LastFacing = Facing.Down;
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
            BiteTween = rb.DOMove(FollowPoint.position, 0.5f).OnPlay(BiteStart).OnComplete(BiteEnd).SetEase(Ease.OutCubic);
        }
        if (!biting)
            rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);

        if (Mathf.Abs(MoveVelocity.x) > Mathf.Abs(MoveVelocity.y))
        {
            LastFacing = (MoveVelocity.x > 0) ? Facing.Right : Facing.Left;
        }
        else if (Mathf.Abs(MoveVelocity.x) < Mathf.Abs(MoveVelocity.y))
        {
            LastFacing = (MoveVelocity.x > 0) ? Facing.Up : Facing.Down;
        }
        //if (Input.GetButtonDown("Left")) LastFacing = Facing.Left;
        //if (Input.GetButtonDown("Right")) LastFacing = Facing.Right;
        //if (Input.GetButtonDown("Up")) LastFacing = Facing.Up;
        //if (Input.GetButtonDown("Down")) LastFacing = Facing.Down;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!biting)
            return;
        if (collision.gameObject.name != "Enemy")
            return;
        collision.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
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
