using UnityEngine;
using System.Collections;

public class RotateAC : MonoBehaviour
{
	private int moveSpeed = 10;
	
	void Update()
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * moveSpeed);
	}
}
