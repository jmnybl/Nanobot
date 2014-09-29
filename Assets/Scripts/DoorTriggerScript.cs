using UnityEngine;
using System.Collections;

public class DoorTriggerScript : MonoBehaviour
{
	public OpenDoor door;
	public float doorOpenUpperBound;
	public float doorOpenLowerBound;
	public float speed;
	public float waitTime;
	public bool activated = false;

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			if(!activated)
			{
				// Local position
				//finalLevel.moveArrow(new Vector3(0, -1.0f, 0));
				door.Open(doorOpenUpperBound, doorOpenLowerBound, speed, waitTime);
				activated = true;
				//gameObject.SetActive(false);
			}
		}
	}
}
