﻿using System.Collections;
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
		/*
		transform.LookAt (target);
		float cameraCurrentDistance = target.position.z - transform.position.z;
		float cameraCurrentHeight = transform.position.y;
		float cameraCurrentDisplacement = target.position.x - transform.position.x;
		float zValue = 0.05f;
		float yValue = 0.05f;
		float xValue = 0.05f;
		if ( cameraCurrentDistance > cameraDistance) {
			zValue = Time.deltaTime * cameraFrontSmoothness * cameraCurrentDistance;
		}
		if (cameraCurrentHeight < cameraHeight) {
			yValue = Time.deltaTime * (cameraHeight - cameraCurrentHeight);
		}
		if (Mathf.Abs(cameraCurrentDisplacement) > cameraLateralDisplacement) {
			xValue = Time.deltaTime * cameraCurrentDisplacement * cameraLateralSmoothness;
		}
		transform.Translate (xValue, yValue,zValue);
		*/
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,target.eulerAngles.y,ref yVelocity,cameraSmoothness);
		Vector3 pos = target.position;
		pos += Quaternion.Euler(0,yAngle,0) * new Vector3(0,cameraHeight,-cameraDistance);
		transform.position = pos;
		transform.LookAt(target);
	}
}
