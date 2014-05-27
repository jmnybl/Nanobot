using UnityEngine;

public class AspectUtility : MonoBehaviour
{
	// 4:3 is 1.333333, 16:10 is 1.6, 16:9 is 1.777778
	public bool landscapeModeOnly = true;
	public static bool _landscapeModeOnly = true;
	private static Camera cam;
	
	void Awake() 
	{
		GameObject newGame = GameObject.Find("New Game Text");
		GameObject exitGame = GameObject.Find("Exit Game Text");
		_landscapeModeOnly = landscapeModeOnly;
		cam = camera;
		if(!cam)
		{
			cam = Camera.main;
		}
		if(!cam)
		{
			return;
		}
		
		float current = GetCurrentAspectRatio();
		if((1.24f < current) && (current < 1.26f))
		{
			newGame.transform.position = new Vector3(-22, 7 , 0);
			exitGame.transform.position = new Vector3(-22, -4, 0);
			cam.transform.position = new Vector3(-15, 0, -42);
		}
		else if((1.32f < current) && (current < 1.34f))
		{
			newGame.transform.position = new Vector3(-14, 7 , 0);
			exitGame.transform.position = new Vector3(-14, -4, 0);
			cam.transform.position = new Vector3(-10, 0, -42);
		}
		else if((1.49f < current) && (current < 1.51f))
		{
			newGame.transform.position = new Vector3(-5, 7 , 0);
			exitGame.transform.position = new Vector3(-5, -4, 0);
			cam.transform.position = new Vector3(-5, 0, -42);
		}
		else if((1.59f < current) && (current < 1.61f))
		{
			newGame.transform.position = new Vector3(-2, 7 , 0);
			exitGame.transform.position = new Vector3(-2, -4, 0);
			cam.transform.position = new Vector3(-5, 0, -42);
		}
		else if((1.77f < current) && (current < 1.79f))
		{
			newGame.transform.position = new Vector3(9, 7 , 0);
			exitGame.transform.position = new Vector3(9, -4, 0);
			cam.transform.position = new Vector3(0, 0, -42);
		}
		else
		{
			Debug.Log("Critical Error! \r\nError code 0001");
			//Debug.Log("\r\nAspect ratio not supported!");
		}
	}
	
	public float GetCurrentAspectRatio()
	{
		float currentAspectRatio = 0.0f;
		if(Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft)
		{
			currentAspectRatio = (float)Screen.width / Screen.height;
		}
		else 
		{
			if(Screen.height  > Screen.width && _landscapeModeOnly) 
			{
				currentAspectRatio = (float)Screen.height / Screen.width;
			}
			else
			{
				currentAspectRatio = (float)Screen.width / Screen.height;
			}
		}
		return currentAspectRatio;
	}
}