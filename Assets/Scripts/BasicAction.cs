using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	//A struct containing data on how much and how likely it is for a certain statusParameter to be influenced by this action.
struct probability{
		//The name of the parameter in question.
	string nameOfStatusParameter;
		//The probability that the statusParameter will be changed by this action.
	float value;
		//The impact at which it is likely that the statusParameter is changed by this action.
	float impact;
}

	//An action that can be performed by the system.
public class BasicAction : MonoBehaviour{
	List<probability> propabilities = new List<probability>();

	public virtual void DoAction(){

	}
}