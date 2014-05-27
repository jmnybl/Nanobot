using UnityEngine;
using System.Collections;

public class RotateFinalLevel : MonoBehaviour
{
	public bool rotate = false;
	public float speed = 1;
	
	private Quaternion target = new Quaternion(0, 0, 0, 1);
	private Quaternion ArrowOriginalRotation;
	private GameObject Arrow;
	private KillCollider killColliderScript;
	
	void Start()
	{
		Arrow = GameObject.Find("arrow").gameObject;
		ArrowOriginalRotation = Arrow.transform.rotation;
		killColliderScript = GetComponent<KillCollider>();
	}
	
	void Update()
	{
		if(rotate)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);
			if(Mathf.Abs(transform.rotation.eulerAngles.z - target.eulerAngles.z) > 359.9)
			{
				GameObject[] platforms = Resources.LoadAll<GameObject>("Platforms");
				if(platforms.Length > 0)
				{
					Sprite greenSprite = platforms[0].GetComponent<SpriteRenderer>().sprite;
					SpriteRenderer currentSprite = gameObject.GetComponent<SpriteRenderer>();
					currentSprite.sprite = greenSprite;
				}
				else
				{
					Debug.Log("Critical Error! \r\nError code 0003");
				}
				rotate = false;
				
				Destroy(killColliderScript);
				
				PolygonCollider2D[] colliders = gameObject.GetComponents<PolygonCollider2D>();
				foreach(PolygonCollider2D collider in colliders)
				{
					if(collider.isTrigger)
					{
						collider.enabled = false;
					}
				}
				
				BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
				boxCollider.isTrigger = true;
				boxCollider.enabled = true;
				gameObject.AddComponent<GoalCollider>();
			}
		}
	}
	
	public void moveArrow(Vector3 localPosition)
	{
		Arrow.transform.parent = transform;
		Arrow.transform.rotation = ArrowOriginalRotation;
		Arrow.transform.localPosition = localPosition;
		Arrow.transform.parent = null;
	}
}
