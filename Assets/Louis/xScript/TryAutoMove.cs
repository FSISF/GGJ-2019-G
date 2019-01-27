using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TryAutoMove : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject target;
    public Node node;
    public Vector2[] Path_v2;
    public float Speed = 1;
    public bool move = true;
    public enum Facing { Up, Down, Left, Right }
    public Facing LastFacing = Facing.Down;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetMouseButtonDown(0))
		//{
		//    Vector2[] path_v2 =  AINavMeshGenerator.pathfinder.FindPath(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		//    List<Vector3> Poss = new List<Vector3>();
		//    foreach(Vector3 n in path_v2)
		//    {
		//        Poss.Add(n);
		//    }
		//    GetComponent<LineRenderer>().positionCount = Poss.Count;
		//    GetComponent<LineRenderer>().SetPositions(Poss.ToArray());
		//}

        Vector2[] TempPath_v2 = AINavMeshGenerator.pathfinder.FindPath(transform.position, target.transform.position);
        if (TempPath_v2 != null)
        {
            Path_v2 = TempPath_v2;
            List<Vector3> Poss = new List<Vector3>();
            foreach (Vector3 n in TempPath_v2)
            {
                Poss.Add(n);
            }
            GetComponent<LineRenderer>().positionCount = Poss.Count;
            GetComponent<LineRenderer>().SetPositions(Poss.ToArray());
            //rb.MovePosition(TempPath_v2[1]);
        }
        if (move)
        {

            Vector2 moveInput = Path_v2[1] - (Vector2)transform.position;
            Vector2 MoveVelocity = moveInput.normalized * Speed;
            rb.MovePosition((Vector2)transform.position + MoveVelocity * Time.fixedDeltaTime);

            if (Mathf.Abs(MoveVelocity.x) > Mathf.Abs(MoveVelocity.y))
            {
                LastFacing = (MoveVelocity.x > 0) ? Facing.Right : Facing.Left;
            }
            else if (Mathf.Abs(MoveVelocity.x) < Mathf.Abs(MoveVelocity.y))
            {
                LastFacing = (MoveVelocity.y > 0) ? Facing.Up : Facing.Down;
            }
            if (MoveVelocity == Vector2.zero)
            {
                animator.Play("Idle" + LastFacing.ToString());
            }
            else
            {
                animator.Play("Walk" + LastFacing.ToString());
            }
        }
    }
}
