using System.Collections;
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
    public SpriteRenderer PlayerSprite;
    public AnimationCurve MoveCurve;
    Tweener BiteTween;
    Collision2D lastCollision;

    List<GameObject> AttackedEnemy = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        //CharManager.Instance.MainChar = GetComponent<CharInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Speed = state.WalkSpeed;
        Vector2 moveInput = new Vector2(MoveCurve.Evaluate(Input.GetAxisRaw("Horizontal")), MoveCurve.Evaluate(Input.GetAxisRaw("Vertical")));
        MoveVelocity = moveInput.normalized * Speed;

    }
    void FixedUpdate()
    {
        
        if (Input.GetMouseButtonDown(0) && CanBite)
        {
            AttackFSM.SendEvent("Bite");
            BiteTween = rb.DOMove(FollowPoint.position, 0.5f).OnPlay(BiteStart).OnComplete(BiteEnd).SetEase(Ease.OutCubic);
            
        }
        if (!biting)
        {
            rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
            if (MoveVelocity == Vector2.zero)
            {
                animator.Play("Idle" + LastFacing.ToString());
            }
            else
            {
                animator.Play("Walk" + LastFacing.ToString());
            }
            PlayerFacing();
        }
        else
        {
            animator.Play("Attack" + LastFacing.ToString());
            PlayerFacing(FollowPoint);
        }

        
    }
    private void LateUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!biting)
            return;
        if (!collision.gameObject.name.Contains("Enemy"))
            return;
        if (AttackedEnemy.Find(x => x.gameObject == collision.gameObject))
            return;
        collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        collision.gameObject.GetComponent<CharacterState>().Hit();
        AttackedEnemy.Add(collision.gameObject);
        Debug.Log(collision.gameObject.name + "Hit");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!biting)
            return;
        if (!collision.gameObject.name.Contains("Enemy"))
            return;
        if (AttackedEnemy.Find(x => x.gameObject == collision.gameObject))
            return;
        collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        collision.gameObject.GetComponent<CharacterState>().Hit();
        AttackedEnemy.Add(collision.gameObject);
        Debug.Log(collision.gameObject.name + "Hit");
    }
    void BiteStart()
    {
        biting = true;
    }
    void BiteEnd()
    {
        biting = false;
    }
    void PlayerFacing()
    {
        if (Mathf.Abs(MoveVelocity.x) > Mathf.Abs(MoveVelocity.y))
        {
            LastFacing = (MoveVelocity.x > 0) ? Facing.Right : Facing.Left;
        }
        else if (Mathf.Abs(MoveVelocity.x) < Mathf.Abs(MoveVelocity.y))
        {
            LastFacing = (MoveVelocity.y > 0) ? Facing.Up : Facing.Down;
        }
    }
    void PlayerFacing(Transform Follow)
    {
        Vector2 followOffset = Follow.position - transform.position;
        if (Mathf.Abs(followOffset.x) > Mathf.Abs(followOffset.y))
        {
            LastFacing = (followOffset.x > 0) ? Facing.Right : Facing.Left;
        }
        else if (Mathf.Abs(followOffset.x) < Mathf.Abs(followOffset.y))
        {
            LastFacing = (followOffset.y > 0) ? Facing.Up : Facing.Down;
        }
    }
    public void CleanAttackedEnemy()
    {
        AttackedEnemy.Clear();
    }
}
