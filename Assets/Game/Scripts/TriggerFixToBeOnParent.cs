using UnityEngine;
using System.Collections;

public class TriggerFixToBeOnParent : MonoBehaviour {
	
	public delegate void StartColliding(Collider collider);
	public event StartColliding StartCollideWithOther;

	public delegate void StopColliding(Collider collider);
	public event StopColliding StopCollideWithOther;

	void OnTriggerEnter(Collider collider){
		StartCollideWithOther(collider);
	}

	void OnTriggerExit(Collider collider){
		StopCollideWithOther(collider);
	}

}
