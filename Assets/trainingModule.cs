using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingModule : MonoBehaviour {

	[SerializeField] private sensorScript[] distanceSensors;
	private string masterLine = "";
	void Awake () {
		GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensors");
		for(int i = 0;i<sensors.Length;i++){
			distanceSensors[i] = sensors[i].GetComponent<sensorScript>();
		}
	}

	void Update () {
		masterLine = string.Concat(masterLine,distanceSensors[0].objectDistance+","+distanceSensors[1].objectDistance+","+distanceSensors[2].objectDistance+","+distanceSensors[3].objectDistance+","+distanceSensors[4].objectDistance+"\n");
		Debug.Log(masterLine);
	}
}
