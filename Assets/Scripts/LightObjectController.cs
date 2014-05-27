using UnityEngine;
using System.Collections;

public class LightObjectController : MonoBehaviour
{
	public void Enable(GameObject previousObject)
	{
		transform.position = previousObject.transform.position;
		gameObject.SetActive(true);
		if(previousObject.rigidbody2D)
		{
			rigidbody2D.velocity = previousObject.rigidbody2D.velocity;
			rigidbody2D.angularVelocity = previousObject.rigidbody2D.angularVelocity;
		}
	}
	
	public void Disable()
	{
		gameObject.SetActive(false);
	}
}
