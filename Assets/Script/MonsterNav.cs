using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TryAutoMove;

public enum NavType
{
	FollowEnemy,
	FarwayEnemy,
	Stop,
	Rand
}

public class MonsterNav : MonoBehaviour
{
	public NavType _nav = NavType.Stop;
	public float Speed = 0.1f;
	public Facing LastFacing = Facing.Down;
	public Animator animator;
	public LineRenderer lineRender;
	public int _index;
	public Vector2[] TempPath_v2;

	private void Awake()
	{
		lineRender = GetComponent<LineRenderer>();
		animator = GetComponent<Animator>();
	}

	public void SwitchNav(NavType nav)
	{
		if (_nav == nav)
			return;
		_nav = nav;
	}

	private void Update()
	{
		if (_nav == NavType.Stop)
		{
			return;
		}
		else if (_nav == NavType.FarwayEnemy)
		{
			Vector2 farNoraml = transform.position - CharManager.Instance.MainChar.transform.position;
			Vector2 mDelta = Speed * farNoraml.normalized * Time.deltaTime;

			transform.position += new Vector3(mDelta.x, mDelta.y, 0f);

			//TODO Use Nav
		}
		else if (_nav == NavType.FollowEnemy)
		{
			//Vector2 farNoraml = transform.position - CharManager.Instance.MainChar.transform.position;
			//Vector2 mDelta = Speed * farNoraml.normalized * Time.deltaTime;
			//transform.position -= new Vector3(mDelta.x, mDelta.y, 0f);

			_index++;
			if (_index % 5 == 0)
			{
				Vector2 toPos = CharManager.Instance.MainChar.transform.position;
				TempPath_v2 = AINavMeshGenerator.pathfinder.FindPath(transform.position, toPos);
			}

			if (TempPath_v2 == null)
			{
				return;
			}

			if (TempPath_v2.Length < 1)
				return;

			List<Vector3> Poss = new List<Vector3>();
			foreach (Vector3 n in TempPath_v2)
			{
				Poss.Add(n);
			}

			if (lineRender != null)
			{
				lineRender.positionCount = Poss.Count;
				lineRender.SetPositions(Poss.ToArray());
			}

			Vector2 moveInput = TempPath_v2[1] - (Vector2)transform.position;
			Vector2 MoveVelocity = moveInput.normalized * Speed * Time.deltaTime;
			transform.position += new Vector3(MoveVelocity.x, MoveVelocity.y, 0);


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

			//TOOD Use Nav
		}
		else if (_nav == NavType.Rand)
		{
			//TODO
		}
	}
}
