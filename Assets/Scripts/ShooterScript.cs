using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour
{
	public Rigidbody2D bullet;
	
	private bool shoot = true;
	private float timer = 2.0f;
	private float force = 10f;

	void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
		if((timer <= 0) && (shoot == false))
		{
			shoot = true;
		}

		if(shoot)
		{
			Rigidbody2D instance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
			// 0 is up
			Vector2 direction = new Vector2(transform.up.x, transform.up.y);
			instance.velocity = direction.normalized * force;
			shoot = false;
			timer = 2.0f;
		}
	}
}
