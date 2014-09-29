using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
	public DoorTriggerScript door_trigger;

	private bool count_door_open = false;
	private float door_wait = 1.0f;
	private bool close_door = false;
	private float door_speed = 1.5f;
	private bool open_door = false;
	private float lower_bound = 0;
	private float upper_bound = 0;

	private float timer = 1.0f;
	void Update()
	{
		if(open_door)
		{
			if((transform.position.y > lower_bound) && !count_door_open && !close_door)
			{
				transform.Translate(-(Vector2.up) * Time.deltaTime * door_speed);
			}
			else
			{
				count_door_open = true;
			}

			if(count_door_open)
			{
				if(timer > 0)
				{
					timer -= Time.deltaTime;
				}

				if(timer <= 0)
				{
					close_door = true;
					count_door_open = false;
				}
			}

			if(close_door && (transform.position.y < upper_bound))
			{
				transform.Translate(Vector2.up * Time.deltaTime * door_speed);
			}
			else if(close_door && !count_door_open && (transform.position.y >= upper_bound))
			{
				close_door = false;
				timer = door_wait;
				open_door = false;
				door_trigger.activated = false;
			}
		}
	}

	public void Open(float upper, float lower, float speed, float wait)
	{
		lower_bound = lower;
		upper_bound = upper;
		door_speed = speed;
		door_wait = wait;
		timer = door_wait;
		open_door = true;
	}
}
