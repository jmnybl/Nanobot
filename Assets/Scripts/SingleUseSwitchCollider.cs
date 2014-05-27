using UnityEngine;
using System.Collections;

public class SingleUseSwitchCollider : MonoBehaviour
{
	public RotateFinalLevel finalLevel;
	
	private bool activated = false;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			if(!activated)
			{
				// Local position
				finalLevel.moveArrow(new Vector3(0, -1.0f, 0));
				finalLevel.rotate = true;
				activated = true;
				//gameObject.SetActive(false);
			}
		}
	}
}
