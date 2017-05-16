using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorScript : MonoBehaviour {

	[SerializeField] private int rayRange = 10;
	[SerializeField] private Color debugColor;
	public float objectDistance = 100.0f;
	void Start () {
		
	}
	
	void FixedUpdate () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		Debug.DrawRay(transform.position,fwd*rayRange,debugColor);
		if(Physics.Raycast(transform.position,fwd,out hit,rayRange)){
			objectDistance = hit.distance;
		}
		else{
			objectDistance = 100.0f;
		}
	}
}
