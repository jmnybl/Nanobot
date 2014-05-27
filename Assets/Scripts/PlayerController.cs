using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
	
	private static GameObject heavyCircle;
	private static GameObject square;
	private static GameObject lightCircle;
	private SquareController squareControl;
	private HeavyObjectController heavyControl;
	private LightObjectController lightControl;
	private bool controlsEnabled = true;
	public static bool isPaused = false;
	
	void Start()
	{
		instance = this;
		square = transform.Find("square").gameObject;
		heavyCircle = transform.Find("heavyCircle").gameObject;
		lightCircle = transform.Find("lightCircle").gameObject;
		squareControl = square.GetComponent<SquareController>();
		heavyControl = heavyCircle.GetComponent<HeavyObjectController>();
		lightControl = lightCircle.GetComponent<LightObjectController>();
		GameController.instance.activePlayer="heavycircle";
	}
	
	void Update()
	{
		if(controlsEnabled && isPaused == false) // do not shift if game is paused
		{
			if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began) // phase limits the shifting only once per touch
			{
				Touch t = Input.GetTouch(0);

				if (t.position.x < (150)) // touch left side of the screen (150px), heavy 
				{
					if (heavyCircle.activeSelf)
					{
						// do nothing
					}
					else if (lightCircle.activeSelf)
					{
						heavyControl.Enable(lightCircle);
						lightControl.Disable();
						GameController.instance.activePlayer="heavycircle";
					}
					else
					{
						heavyControl.Enable(square);
						squareControl.Disable();
						GameController.instance.activePlayer="heavycircle";
					}
				}
				else if (t.position.x>(Screen.width-150))
				{
					if (heavyCircle.activeSelf) // touch right, light
					{
						lightControl.Enable(heavyCircle);
						heavyControl.Disable();
						GameController.instance.activePlayer="lightcircle";
					}
					else if (lightCircle.activeSelf)
					{
						// do nothing
					}
					else
					{
						lightControl.Enable(square);
						squareControl.Disable();
						GameController.instance.activePlayer="lightcircle";
					}
				}
				else
				{
					if (heavyCircle.activeSelf) // touch middle, magnet
					{
						squareControl.Enable(heavyCircle);
						heavyControl.Disable();
						GameController.instance.activePlayer="square";
					}
					else if (lightCircle.activeSelf)
					{
						squareControl.Enable(lightCircle);
						lightControl.Disable();
						GameController.instance.activePlayer="square";
					}
					else
					{
						// do nothing
					}

				}
			}
			
		} // android controls end

		// computer controls for changing player, comment out if these disturbs android
		if(controlsEnabled && isPaused==false)
		{
			if(Input.GetKey("1"))
			{
				if(square.activeSelf)
				{
					heavyControl.Enable(square);
					squareControl.Disable();
				}
				else if(lightCircle.activeSelf)
				{
					heavyControl.Enable(lightCircle);
					lightControl.Disable();
				}
			}
			else if(Input.GetKey("2"))
			{
				if(heavyCircle.activeSelf)
				{
					squareControl.Enable(heavyCircle);
					heavyControl.Disable();
				}
				else if(lightCircle.activeSelf)
				{
					squareControl.Enable(lightCircle);
					lightControl.Disable();
				}
			}
			else if(Input.GetKey("3"))
			{
				if(heavyCircle.activeSelf)
				{
					lightControl.Enable(heavyCircle);
					heavyControl.Disable();
				}
				else if(square.activeSelf)
				{
					lightControl.Enable(square);
					squareControl.Disable();
				}
			}
		} // computer controls end



	}
	
	public void Die()
	{
		controlsEnabled = false;
		if(heavyCircle.activeSelf)
		{
			heavyCircle.rigidbody2D.gravityScale = 0;
			heavyCircle.rigidbody2D.velocity = Vector2.zero;
			heavyCircle.rigidbody2D.angularVelocity = 0f;
		}
		else if(lightCircle.activeSelf)
		{
			lightCircle.rigidbody2D.gravityScale = 0;
			lightCircle.rigidbody2D.velocity = Vector2.zero;
			lightCircle.rigidbody2D.angularVelocity = 0f;
		}
		else if(square.activeSelf)
		{
			square.rigidbody2D.gravityScale = 0;
			square.rigidbody2D.velocity = Vector2.zero;
			square.rigidbody2D.angularVelocity = 0f;
		}
		GameController.instance.EndGame();
		StartCoroutine("Wait");
	}
	
	public void CompleteLevel()
	{
		GameController.instance.EndLevel();
		StartCoroutine("Wait");
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(5.0f);
		GameController.instance.EndScene();
	}
}
