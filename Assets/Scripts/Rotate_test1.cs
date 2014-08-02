using UnityEngine;
using System.Collections;

public class Rotate_test1 : MonoBehaviour
{
	private float moveSpeed = 2.4444444f;
	
	void Update()
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * moveSpeed);
	}
}
