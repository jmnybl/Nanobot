using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SquareController : MonoBehaviour
{
	public static SquareController instance;
	
	private int RaysToShoot = 1024; //64; 128; 1024; 	10240; etc.
	private bool castRays = false;
	private RaycastHit2D shortestHit;
	private float forceMultiplier = 10;
	
	private bool newcollision = false;
	private Collision2D collision = null;
	private bool kinematic = false;
	
	private CollisionDetectionMode2D mode;
	private float mass;
	private float drag;
	private float angularDrag;
	private float gravityScale;
	
	void Start()
	{
		instance = this;
		mode = rigidbody2D.collisionDetectionMode;
		mass = rigidbody2D.mass;
		drag = rigidbody2D.drag;
		angularDrag = rigidbody2D.angularDrag;
		gravityScale = rigidbody2D.gravityScale;
	}
	
	void Update()
	{
		if(newcollision && collision.transform)
		{
			//ContactPoint2D[] contacts = coll.contacts;
			//foreach(ContactPoint2D p in contacts)
			//{
				//Debug.Log(p.point);
			//}
			//ContactPoint2D v2cPoint = collision.contacts[0];
			//Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, v2cPoint.normal);
			//transform.rotation = targetRot;
			transform.parent.parent = collision.transform;
			Destroy(rigidbody2D);
			//gameObject.GetComponent<CircleCollider2D>().enabled = false;
			rigidbody2D.isKinematic = true;
			newcollision = false;
			//Physics2D.IgnoreLayerCollision(8, 9, true);
		}
	}
	
	void FixedUpdate()
	{
		if(castRays)
		{
			int oldLayer = gameObject.layer;
			gameObject.layer = Physics2D.IgnoreRaycastLayer;
			int layerToIgnore = (1 << gameObject.layer) + (1 <<  2); // 2 is 'ignore raycast' layer aka non-magnetic surface
			layerToIgnore = ~layerToIgnore;
		
			float angle = 0;
			List<RaycastHit2D> rays = new List<RaycastHit2D>();
			for(int i = 0; i < RaysToShoot; i++)
			{
				float x = Mathf.Sin(angle);
				float y = Mathf.Cos(angle);
				angle += 2*Mathf.PI/RaysToShoot;
			
				Vector2 dir = new Vector2(x, y);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, layerToIgnore);
				if(hit.collider != null)
				{
					rays.Add(hit);
				}
			}
			
			rays.Sort(delegate(RaycastHit2D i1, RaycastHit2D i2)
			{ 
				return i1.fraction.CompareTo(i2.fraction); 
			});
			shortestHit = rays[0];
		
			Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, shortestHit.normal);
			transform.rotation = targetRot;
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
			gameObject.layer = oldLayer;
			castRays = false;
		}
		else
		{
			if(!kinematic)
			{
				Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
				Vector2 forceVector = (shortestHit.point - position2D).normalized;
				rigidbody2D.AddForce(forceVector * forceMultiplier);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Level") {
			if (!kinematic) {
				newcollision = true;
				collision = coll;
				kinematic = true;
				}
			} else {
				castRays = true;
			}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		// if a nonmagnetic surface moves player to different direction, this one recalculates the closest magnetic surface
		if (coll.gameObject.tag != "Level") {
			castRays = true;
		}
	}
	
	public void Enable(GameObject previousObject)
	{
		castRays = true;
		transform.position = previousObject.transform.position;
		gameObject.SetActive(true);
		//rigidbody2D.velocity = previousObject.rigidbody2D.velocity;
		//rigidbody2D.angularVelocity = previousObject.rigidbody2D.angularVelocity;
	}
	
	public void Disable()
	{
		transform.parent.parent = null;
		//Physics2D.IgnoreLayerCollision(8, 9, false);
		//gameObject.GetComponent<CircleCollider2D>().enabled = true;
		if(kinematic)
		{
			gameObject.AddComponent<Rigidbody2D>();
			rigidbody2D.collisionDetectionMode = mode;
			rigidbody2D.mass = mass;
			rigidbody2D.drag = drag;
			rigidbody2D.angularDrag = angularDrag;
			rigidbody2D.gravityScale = gravityScale;
			kinematic = false;
		}
		gameObject.SetActive(false);
	}
}
