using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CharInterface charInterface = collision.GetComponent<CharInterface>();
		if (charInterface == null)
		{
			return;
		}

		charInterface.TakeDamage(damage);
		Destroy(gameObject);
	}
}
