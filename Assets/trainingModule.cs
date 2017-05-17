using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingModule : MonoBehaviour {

	[SerializeField] private int frameDelay = 30;
	[SerializeField] private string url = "localhost:5000/reinforce?values=";
	[SerializeField] private sensorScript[] distanceSensors;
	private int framesElapsed = 0;
	private int customFrameElapsed = 0;
	void Awake () {
		GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensors");
		for(int i = 0;i<sensors.Length;i++){
			distanceSensors[i] = sensors[i].GetComponent<sensorScript>();
		}
	}

	void customUpdate(){
		string masterLine = "";
		masterLine = string.Concat(masterLine,distanceSensors[0].objectDistance+","+distanceSensors[1].objectDistance+","+distanceSensors[2].objectDistance+","+distanceSensors[3].objectDistance+","+distanceSensors[4].objectDistance);
		//Debug.Log(masterLine);
		sendVariables(masterLine);
	}

	private void sendVariables(string master){
		string updatedURL = string.Concat(url,master);
		WWW www = new WWW(updatedURL);
		StartCoroutine(WaitForRequest(www));
	}
	IEnumerator WaitForRequest(WWW www){
		yield return www;
		if(www.error == null){
		} 
		else {
			Debug.Log("error: "+www.error);
		}
	}
	void Update () {
		framesElapsed++;
		if(framesElapsed%frameDelay == 0){
			customFrameElapsed++;
			customUpdate();
		}
	}

	
}
