using UnityEngine;
using System.Collections;

public class SwitchCollider : MonoBehaviour
{
	public LevelController nextLayer;

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.collider.tag == "Player"  && !nextLayer.isActive)
		{
			//Debug.Log("OnCollisionEnter2D");
			// Activate nextLayer
			nextLayer.isActive = true;
			
			//Deactivate all the rest layers
			LevelController[] layers = GameObject.FindObjectsOfType(typeof(LevelController)) as LevelController[];
			foreach(LevelController layer in layers)
			{
				if(layer.name != nextLayer.name)
				{
					layer.isActive=false;
				}
			}
		}
	}
	
	void OnCollisionStay2D(Collision2D c)
	{
		if(c.collider.tag == "Player" && !nextLayer.isActive)
		{
			//Debug.Log("OnCollisionStay2D");
			// Activate nextLayer
			nextLayer.isActive = true;
			
			//Deactivate all the rest layers
			LevelController[] layers = GameObject.FindObjectsOfType(typeof(LevelController)) as LevelController[];
			foreach(LevelController layer in layers)
			{
				if(layer.name != nextLayer.name)
				{
					layer.isActive=false;
				}
			}
		}
	}
}
