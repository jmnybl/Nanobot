using UnityEngine;
using System.Collections;

public class KillCollider : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Player")
		{
			return;
		}
		PlayerController.instance.Die();
	}
}
