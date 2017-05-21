using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothFollowCamera : MonoBehaviour {

	public Transform target;

	public float cameraHeight = 1.0f;
	public float cameraDistance = 1.0f;
	public float cameraFrontSmoothness = 1.0f;
	public float cameraLateralDisplacement = 1.0f;
	public float cameraSmoothness = 1.0f;
	private float yVelocity = 0.0f;
	void Awake () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () {
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,target.eulerAngles.y,ref yVelocity,cameraSmoothness);
		Vector3 pos = target.position;
		pos += Quaternion.Euler(0,yAngle,0) * new Vector3(0,cameraHeight,-cameraDistance);
		transform.position = pos;
		transform.LookAt(target);
	}
}
