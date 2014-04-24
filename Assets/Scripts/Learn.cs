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
			for(int i = 0; i < actionJustPerformed.probabilities.Count; i++){
					//find out the impact the last action had on a status parameter.
				if(agentController.perceptionMemory[0][actionJustPerformed.probabilities[i].nameOfStatusParameter].parameterType == ParameterTypes.Float){
					float parameterImpact = (float)agentController.perceptionMemory[0][actionJustPerformed.probabilities[i].nameOfStatusParameter].Value - (float)agentController.perceptionMemory[1][actionJustPerformed.probabilities[i].nameOfStatusParameter].Value;

					actionJustPerformed.probabilities[i].influenceProbability(parameterImpact);
				}
			}
		}
	}
}