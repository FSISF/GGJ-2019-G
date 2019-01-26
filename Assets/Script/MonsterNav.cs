using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			Vector2 farNoraml = transform.position - CharManager.Instance.MainChar.transform.position;
			Vector2 mDelta = Speed * farNoraml.normalized * Time.deltaTime;
			transform.position -= new Vector3(mDelta.x, mDelta.y, 0f);

			//TOOD Use Nav
		}
		else if (_nav == NavType.Rand)
		{
			//TODO
		}
	}
}
