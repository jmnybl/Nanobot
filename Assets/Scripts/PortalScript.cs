using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour
{
	public PortalScript portal2nd;
	public bool pass = false;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player" && !pass)
		{
			portal2nd.pass = true;
			float speed = c.rigidbody2D.velocity.magnitude;

			c.transform.position = portal2nd.transform.position;
			Vector2 direction = new Vector2(portal2nd.transform.up.x, portal2nd.transform.up.y);
			c.rigidbody2D.velocity = direction.normalized * speed;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.tag == "Player" && pass)
		{
			pass = false;
		}
	}
}
