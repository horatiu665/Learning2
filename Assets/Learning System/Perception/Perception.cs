using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using WorldState = System.Collections.Generic.Dictionary<string, StatusParameter>; // this replaces Dictionary<...> with WorldState


// other partial Perception classes will contain the statusParameter getter functions
public partial class Perception : MonoBehaviour
{

	public AgentController agentController;

	/// <summary>
	/// this dictionary represents the 'world state' as interpreted by the AI.
	/// the format of the corresponding update function is "Get" + the name of the variable in the dictionary.
	/// </summary>
	public Dictionary<string, StatusParameter> statusParameters;

	// Fill the dictionary statusParameters with all the status parameters in the different goals at system-startup.
	public void InitializePerception()
	{
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
					
				} else {
					// make a reference to the existing entry in the goal parameter.
					gp.statusParameter = statusParameters[gp.parameterName];

				}
			}
		}

		// initialize extra parameters not used in any goals here

	}

	///<summary>Call this class every time the perception part needs to run.</summary>
	public void IteratePerception()
	{
		// update all status parameters to represent the current world state

		// call all functions in the partial class
		// with name "Get" + each parameter name
	}
}