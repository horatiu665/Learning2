using UnityEngine;
using System.Collections;

public class TriggerFixToBeOnParent : MonoBehaviour {
		//Making two custom events
	public delegate void StartColliding(Collider collider);
	public event StartColliding StartCollideWithOther;
	public delegate void StopColliding(Collider collider);
	public event StopColliding StopCollideWithOther;


		//When OntriggerEnter is called, activate the StartCollideWithOther-event
	void OnTriggerEnter(Collider collider){
		StartCollideWithOther(collider);
	}


		//When OntriggerExit is called, activate the StopCollideWithOther-event
	void OnTriggerExit(Collider collider){
		StopCollideWithOther(collider);
	}
}