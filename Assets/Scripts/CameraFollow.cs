using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour
{
    private static GameObject player;
    private static GameObject square;
    private static GameObject heavyCircle;
    private static GameObject lightCircle;
	private float cameraDistance = -10.0f;
	private float zoom_out = 100.0f;
	private float zoom_in_max = 50.0f;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        square = player.transform.Find("square").gameObject;
        heavyCircle = player.transform.Find("heavyCircle").gameObject;
        lightCircle = player.transform.Find("lightCircle").gameObject;
    }
    
    void Update()
    {


        if (square.activeSelf) {
			// this has no rigidbody, so fixed camera zooming
			transform.position = new Vector3 (square.transform.position.x, square.transform.position.y, cameraDistance);
			camera.fieldOfView = zoom_out;

		} 
		else {
			Vector2 vel = new Vector2(0,0);
			if (heavyCircle.activeSelf) {
				transform.position = new Vector3 (heavyCircle.transform.position.x, heavyCircle.transform.position.y, cameraDistance);
				vel = heavyCircle.rigidbody2D.velocity;
			}
			else if (lightCircle.activeSelf) {
				transform.position = new Vector3 (lightCircle.transform.position.x, lightCircle.transform.position.y, cameraDistance);
				vel = lightCircle.rigidbody2D.velocity;
			}
			else {
				vel = new Vector2(0,0);
			}
			// TODO: smoothing
			float speed = (Math.Abs (vel.x) + Math.Abs (vel.y)) * 20; // calculate the 'actual' speed and multiply it with 'something' to get suitable zooming
			if (zoom_in_max > speed) {
				camera.fieldOfView = zoom_in_max;
			} 
			else if (zoom_out < speed) {
				camera.fieldOfView = zoom_out;
			} 
			else {
				camera.fieldOfView = speed;
			}
			
		}
    } 
}
