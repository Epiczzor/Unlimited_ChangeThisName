using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackController : MonoBehaviour {

	private bool firstCheck;
	public GameObject trackGod;
	void Awake () {
		trackGod = GameObject.FindGameObjectWithTag ("GameController");
		firstCheck = false;
	}
		
	void OnCollisionExit(Collision col){
		if (col.gameObject.CompareTag ("Player")) {
			firstCheck = true;
			Invoke ("WaitForExit",7);
		}
	}

	void WaitForExit(){
		if (firstCheck)
			DestroyThisTrack ();
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Player")) {
			firstCheck = false;
		}
	}
	void DestroyThisTrack(){
		trackGod.SendMessage ("DestroyLastTrack");
	}
}
