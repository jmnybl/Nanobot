using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour
{
	void OnMouseEnter()
	{
		renderer.material.color = Color.grey;
		gameObject.animation.Play("ResizeUp");
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
		gameObject.animation.Play("ResizeDown");
	}
	
	void OnMouseDown()
	{
		Application.LoadLevel("Level1");
	}
}
