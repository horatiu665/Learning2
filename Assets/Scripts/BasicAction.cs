using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	//A struct containing data on how much and how likely it is for a certain statusParameter to be influenced by this action.
public class Probability{
		//The name of the parameter in question.
	public string nameOfStatusParameter;
		//The probability that the statusParameter will be changed by this action.
	public float value;
		//The impact at which it is likely that the statusParameter is changed by this action.
	public float impact;
}

	//An action that can be performed by the system.
public class BasicAction : MonoBehaviour{
	public List<Probability> propabilities = new List<Probability>();

	public virtual void DoAction(){

	}
}