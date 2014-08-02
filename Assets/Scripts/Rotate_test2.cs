using UnityEngine;
using System.Collections;

public class Rotate_test2 : MonoBehaviour
{
	private float moveSpeed = 1f;
	
	void Update()
	{
		transform.Rotate(Vector3.back * Time.deltaTime * moveSpeed);
	}
}
