using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (shipMotorScript))]
public class shipUserControl : MonoBehaviour {

	private shipMotorScript shipMotor;
	void Awake () {
		shipMotor = GetComponent<shipMotorScript> ();
	}
	

	public void FixedUpdate () {

		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		shipMotor.moveShip (h, v);
	}
}
