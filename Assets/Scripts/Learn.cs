using UnityEngine;
using System.Collections;

public class Learn{
		//A reference to the class initializing an instance of this class (The AgentController). This class should be filled with the reference by the 'container'-class in Awake.
	public AgentController agentController;

		///<summary>Call this class every time the learn part needs to run.</summary>
	public void IterateLearn(){
			//If any actions have ever been performed.
		if(agentController.actionsMemory.Count > 0){
				//Finds the newest action performed by the system.
			BasicAction actionJustPerformed = agentController.actionsMemory[0];

				//foreach statusparameter in the last action action, find out the impact the action had and act accordingly.
			for(int i = 0; i < actionJustPerformed.propabilities.Count; i++){
					//find out the impact the last action had on a status parameter.
				if(agentController.perceptionMemory[0][actionJustPerformed.propabilities[i].nameOfStatusParameter].parameterType == ParameterTypes.Float){
					float parameterImpact = (float)agentController.perceptionMemory[0][actionJustPerformed.propabilities[i].nameOfStatusParameter].Value - (float)agentController.perceptionMemory[1][actionJustPerformed.propabilities[i].nameOfStatusParameter].Value;

					if(parameterImpact > 0){
						actionJustPerformed.propabilities[i].value += 0.1f;
					}
					else if(parameterImpact < 0){
						actionJustPerformed.propabilities[i].value -= 0.1f;
					}
					else{
						actionJustPerformed.propabilities[i].value -= 0.01f * Mathf.Sign(actionJustPerformed.propabilities[i].value);
					}
				}
				else if(agentController.perceptionMemory[0][actionJustPerformed.propabilities[i].nameOfStatusParameter].parameterType == ParameterTypes.Bool){
					bool firstMemory = (bool)agentController.perceptionMemory[0][actionJustPerformed.propabilities[i].nameOfStatusParameter].Value;
					bool secondMemory = (bool)agentController.perceptionMemory[1][actionJustPerformed.propabilities[i].nameOfStatusParameter].Value;
					if(firstMemory == true && secondMemory == false){
						actionJustPerformed.propabilities[i].value += 0.1f;
					}
					else if(firstMemory == false && secondMemory == true){
						actionJustPerformed.propabilities[i].value -= 0.1f;
					}
					else{
						actionJustPerformed.propabilities[i].value -= 0.01f * Mathf.Sign(actionJustPerformed.propabilities[i].value);
					}
				}
			}
		}
	}
}