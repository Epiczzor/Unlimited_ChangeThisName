using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMotorScript : MonoBehaviour {


	private Rigidbody shipRigidbody;

	[SerializeField] private float torqueMultiplier = 1.0f;
	[SerializeField] private float downForce = 1.0f;
	[SerializeField] private float speedCap = 10.0f;
	[SerializeField] private float steerSpeed = 50.0f;
	[SerializeField] private float flyingDistance = 1.0f;
	[SerializeField] private float maxSteerAngle = 25.0f;


	private float autoCorrectionFactor = 0.3f;
	private float resetThreshold = 2.0f;
	private Vector3 oldPosition;
	private int[] errorCounter = {0,0,0,0,0}; 
	private bool trackFound = false;
	private int flyingThreshold = 2;
	public int carFlying = 0;
	private float ySmooth = 0.7f;

	private Vector3 lastContactPos;
	void Start () {
		shipRigidbody = GetComponent<Rigidbody> ();
		oldPosition = transform.position;
		InvokeRepeating("checkCarStatus",3.0f,1.0f);
	}

	void Update () {
		autoAngleCorrection ();
	}


	private void trackFloating(){
		RaycastHit hit;
		float yVelo = 0.0f;
		if(Physics.Raycast(transform.position,Vector3.down,out hit,flyingThreshold)){
			//float yvalue = Mathf.SmoothDamp(transform.position.y,flyingDistance,ref yVelo,ySmooth);
			//shipRigidbody.AddExplosionForce(1000.0f,hit.point,5.0f,5.0f);
		}
	}
	public void moveShip(float steer,float torque){
		
		steer = Mathf.Clamp (steer,-1, 1);
		//Debug.Log("Steer:"+steer);
		//Debug.Log("Local Rotation:"+transform.rotation.eulerAngles);
		torque = Mathf.Clamp (torque, 0, 1);
		if(steer != 0) rotationDueToSteer (steer);
		if (shipRigidbody.velocity.magnitude < speedCap) {
			shipRigidbody.AddForce (transform.forward * torque * torqueMultiplier);
		}
		//shipRigidbody.AddForce (transform.right * steerMultiplier * steer);
		addDownForce ();
		//trackFloating();
	}

	/*
	private void OnCarFly(){
		if(!trackFound) errorCounter[1]++;
	}
	void OnCollisionExit(Collision col){
		if(col.gameObject.CompareTag("Track") && trackFound){
			trackFound = false;
			Invoke("OnCarFly",3.0f);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Track")){
			trackFound = true;
			errorCounter[1] = 0;
		}
	}
	*/
	private void addDownForce(){
		float jumpStop = 1.0f;
		jumpStop *= transform.position.y;
		shipRigidbody.AddForce (Vector3.up * -1 * downForce);
	}

	private void autoAngleCorrection(){
		float velo = 0.0f;
		float smoothF = 1.0f;
		float xValue = Mathf.SmoothDamp(transform.rotation.x,0.0f,ref velo,smoothF);
		float zValue = Mathf.SmoothDamp(transform.rotation.z,0.0f,ref velo,smoothF);
		Vector3 pos = transform.position;
		pos += Quaternion.Euler(xValue,0,zValue) * new Vector3(0,0,0);
		transform.position = pos;
	}

	private void checkCarStatus(){
		
		if(!Physics.Raycast(transform.position,Vector3.down,flyingThreshold)) {
			carFlying = 1;
			errorCounter[1]++;
		}
		else {
			lastContactPos = transform.position;
			errorCounter[1] = 0;
			carFlying = 0;
		}
		if(Vector3.Distance(oldPosition,transform.position) < 3.0f){
			errorCounter[0]++;
			if(errorCounter[0] > 4){
				Debug.Log("reset car");
			}
		}
		if(transform.rotation.eulerAngles.z >= 170 && transform.rotation.eulerAngles.z <= 220) {
			errorCounter[2]++;
			if(errorCounter[2] >= 4){
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,0.0f);
				transform.position = new Vector3(transform.position.x,transform.position.y+4.0f,transform.position.z);
			}
		}
		if(errorCounter[1] > 4){
			Debug.Log("Car Flying Reset Car");
			transform.rotation = Quaternion.Euler(0,0,0);
			transform.position = lastContactPos;
		}
		oldPosition = transform.position;
	}
	private void rotationDueToSteer(float steer){
		float currentAngle = transform.rotation.eulerAngles.y;
		transform.Rotate (0.0f,steer * Time.deltaTime * steerSpeed, 0.0f);
		shipRigidbody.AddForce (transform.right * steer * 2);
	}

}
