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

	public void influenceProbability(float parameterImpact){
		if(impact == 0){
			impact = parameterImpact;
		}
		else{
			impact *= (parameterImpact / impact) * Mathf.Sign (value);
		}
	}
}

	//An action that can be performed by the system.
public class BasicAction : MonoBehaviour{
		//A list of the probability of impact this action has on each statusParameter.
	public List<Probability> probabilities = new List<Probability>();

	public virtual IEnumerator DoAction(){
		yield return 0;
	}
}