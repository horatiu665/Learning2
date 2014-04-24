using UnityEngine;
using System.Collections;

	///<summary>Predict which action should be done next based on utility and probability</summary>
public class Predict{
		//A reference to the class initializing an instance of this class (The AgentController). This class should be filled with the reference by the 'container'-class in Awake.
	public AgentController agentController;

		///<summary>Call this class every time the predict part needs to run.</summary>
	public BasicAction IteratePredict(){
		BasicAction actionToPerform = agentController.actions[Random.Range(0, agentController.actions.Count)];
		return actionToPerform;
	}
}