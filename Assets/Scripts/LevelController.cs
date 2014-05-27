using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
	public Rigidbody2D hinge;
	public static bool gamePaused = false;
	public bool isActive;
	public bool flipXAxis = true;

	private float moveSpeed = 3500000000f;
	private float lastGradient  = 0f;
	private float currentAngle = 0f; // needed for computer controls
	
	void Awake()
	{
		HingeJoint2D hj = gameObject.AddComponent<HingeJoint2D>();
		
		//hj.useLimits = true;
		//JointAngleLimits2D lim = hj.limits;
		//lim.min = 0;
		//lim.max = 90;
		//hj.limits = lim;
		
		//hj.useMotor = true;
		//JointMotor2D motor = hj.motor;
		//motor.maxMotorTorque = 10000f;
		//motor.motorSpeed = 22000;
		//hj.motor = motor;
		
		hj.connectedBody = hinge;
		hj.collideConnected = false;

	}
	
	void FixedUpdate()
	{
		if(!gamePaused)
		{
			if(isActive == true)
			{
				//float targetAngle = Input.GetAxis("Horizontal") * (flipXAxis ? -1 : 1);
				//Vector3 gradient = Vector3.zero;
				float gradient = Input.acceleration.x;
				//gradient.Normalize();
				if ((gradient > 0.1 && gradient < 0.6) || (gradient < -0.1 && gradient > -0.6)) // update only if gradien between hmm, what ?
				{

					float targetAngle = gradient * (flipXAxis ? -1 : 1); // listen accelerometer instead of keyboard
					rigidbody2D.AddTorque(targetAngle * moveSpeed * Time.deltaTime);
					lastGradient = gradient;
				}
				else if (gradient > 0.6 || gradient < -0.6) // is the angle too big?
				{
					float targetAngle = lastGradient * (flipXAxis ? -1 : 1); // use the previous gradient
					rigidbody2D.AddTorque(targetAngle * moveSpeed * Time.deltaTime);

				}
			} // android controls end

			// computer controls for rotating level, comment out if these disturbs android
			if(isActive == true)
			{
				float targetAngle = Input.GetAxis("Horizontal") * (flipXAxis ? -1 : 1);
				currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);
				transform.Rotate(new Vector3 (0, 0, currentAngle));
			} // computer controls end

		}
	}
	
	/*void Update()
	{
		if(!gamePaused)
		{
			if(isActive)
			{
				float targetAngle = Input.GetAxis("Horizontal") * (flipXAxis ? -1 : 1);
				currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);
				transform.Rotate(new Vector3(0, 0, currentAngle) * moveSpeed * Time.deltaTime);
			}
		}
	}*/
}
