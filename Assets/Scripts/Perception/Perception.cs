using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// other partial Perception classes will contain the statusParameter getter functions
public partial class Perception : MonoBehaviour
{

	AgentController agentController;

	//A dictionary of the variables used by the system's goals. the format of the corresponding update function is "Get" + the name of the variable in the dictionary.
	Dictionary<string, StatusParameter> statusParameters;

	//Fill the dictionary statusParameters with all the status parameters in the different goals at system-startup.
	public void InitializePerception()//AgentController agentController)
	{
		//this.agentController = agentController;

		statusParameters = new Dictionary<string, StatusParameter>();
		// for each goal
		foreach (var goal in agentController.goals) {
			// for each goal parameter
			foreach (var gp in goal.goalParameters) {
				// if we didn't make an entry with that name
				if (!statusParameters.ContainsKey(gp.parameterName)) {
					// add a new entry to the dictionary
					statusParameters.Add(gp.parameterName, new StatusParameter(gp.statusParameter));
					// make a reference to the new entry in the goal parameter
					gp.statusParameter = statusParameters[gp.parameterName];
					print(gp.parameterName + " has " + gp.multiplier);

				} else {
					// make a reference to the existing entry in the goal parameter.
					gp.statusParameter = statusParameters[gp.parameterName];

				}
			}
		}

	}

	///<summary>Call this class every time the perception part needs to run.</summary>
	public void IteratePerception()
	{
		
	}
}