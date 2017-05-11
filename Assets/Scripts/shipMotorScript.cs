using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMotorScript : MonoBehaviour {


	private Rigidbody shipRigidbody;

	[SerializeField] private float torqueMultiplier = 1.0f;
	[SerializeField] private float downForce = 1.0f;
	[SerializeField] private float speedCap = 10.0f;
	[SerializeField] private float steerMultiplier = 1.0f;
	[SerializeField] private float heightLimiter = 1.0f;
	[SerializeField] private float maxSteerAngle = 25.0f;

	private float rotationSpeed = 75.0f;
	private float autoCorrectionFactor = 1.0f;
	void Start () {
		shipRigidbody = GetComponent<Rigidbody> ();
	}

	void Update () {
		autoAngleCorrection ();
	}

	public void moveShip(float steer,float torque){
		
		steer = Mathf.Clamp (steer,-1, 1);
		torque = Mathf.Clamp (torque, 0, 1);
		rotationDueToSteer (steer);
		if (shipRigidbody.velocity.magnitude < speedCap) {
			shipRigidbody.AddForce (transform.forward * torque * torqueMultiplier);
		}
		shipRigidbody.AddForce (transform.right * steerMultiplier * steer);
		addDownForce ();
	}

	private bool isCarFlying(){
		if (transform.position.y > heightLimiter)
			return true;
		else
			return false;
			
	}

	private void addDownForce(){
		float jumpStop = 1.0f;
		if (isCarFlying())
			jumpStop *= transform.position.y;
		shipRigidbody.AddForce (transform.up * -1 * downForce * jumpStop);
	}

	private void autoAngleCorrection(){
		/*
		Debug.Log(transform.rotation.z);
		float xValue = 0.0f,zValue = 0.0f;
		if (transform.rotation.eulerAngles.x > 2 || transform.rotation.eulerAngles.x < -2)
			xValue = transform.rotation.x * -1.0f * autoCorrectionFactor;
		if (transform.rotation.eulerAngles.z > 2 || transform.rotation.eulerAngles.z < -2)
			zValue = transform.rotation.z * -1.0f * autoCorrectionFactor;
		transform.Rotate (Mathf.Lerp(xValue,0.0f,Time.deltaTime), 0.0f,Mathf.Lerp(zValue,0.0f,Time.deltaTime));
		*/

		//transform.Rotate (transform.rotation.eulerAngles.x * -1.0f * autoCorrectionFactor, 0.0f,transform.rotation.eulerAngles.z * -1.0f * autoCorrectionFactor);
	}
	private void rotationDueToSteer(float steer){
		
		float currentAngle = transform.rotation.y;
		//Debug.Log (transform.rotation.y);
		if (currentAngle < maxSteerAngle && currentAngle > (-1 * maxSteerAngle)) {
			transform.Rotate (0.0f,steer * Time.deltaTime * rotationSpeed, 0.0f);
		}
	}

}
