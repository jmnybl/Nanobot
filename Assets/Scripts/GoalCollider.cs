using UnityEngine;
using System.Collections;

public class GoalCollider : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Player")
		{
			return;
		}
		PlayerController.instance.CompleteLevel();
	}
}
