using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTrackGenerator : MonoBehaviour {

	[SerializeField] private GameObject[] trackMesh;
	[SerializeField] private float trackInterval = 1.0f;
	[SerializeField] private int maxActiveTracks = 5;

	private GameObject playerObject;
	private float trackDisplacement = 100;
	private GameObject prevTrack;
	private Queue<GameObject> currentTracks;
	private bool destroyedLast;

	void Awake () {
		currentTracks = new Queue<GameObject>();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		GenerateTrack ();
	}

	private void GenerateTrack(){
		Transform spawn = playerObject.transform;
		prevTrack = Instantiate (trackMesh [0],new Vector3(spawn.position.x,spawn.position.y-1.0f,spawn.position.z + 100),trackMesh[0].transform.rotation);
		currentTracks.Enqueue (prevTrack);
		SpawnRandomTracks ();
	}

	private void SpawnRandomTracks(){
		if (currentTracks.Count < maxActiveTracks) {
			destroyedLast = false;
			Transform nextSpawn = prevTrack.transform;
			prevTrack = Instantiate (trackMesh [Random.Range (0, trackMesh.Length)], new Vector3 (nextSpawn.position.x, nextSpawn.position.y, nextSpawn.position.z + trackDisplacement), nextSpawn.rotation);
			StoreThisTrack (prevTrack);
		}
		Invoke ("SpawnRandomTracks", trackInterval);
	}

	private void StoreThisTrack(GameObject obj){
		currentTracks.Enqueue (obj);
	}

	public void DestroyLastTrack(){
		if (!destroyedLast) {
			GameObject del = currentTracks.Dequeue ();
			deleteGameObject (del);
			destroyedLast = true;
		}
	}

	void deleteGameObject(GameObject ob){
		Destroy (ob);
	}

	private void StopTrackGeneration(){
		CancelInvoke ();
	}
}
