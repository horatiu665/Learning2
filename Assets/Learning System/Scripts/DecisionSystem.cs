using UnityEngine;
using System.Collections;

///<summary>Predict which action should be done next based on utility and probability</summary>
public class DecisionSystem
{
	//A reference to the class initializing an instance of this class (The AgentController). This class should be filled with the reference by the 'container'-class in Awake.
	public AgentController agentController;

	public enum DecisionModes
	{
		Random,
		MaxUtility
	}
	public DecisionModes decisionMode = DecisionModes.MaxUtility;

	[Range(1, 7)]
	public int stepsAhead = 1;

	///<summary>Call this class every time the predict part needs to run.</summary>
	public BasicAction IterateDecision()
	{
		if (decisionMode == DecisionModes.Random) {
			return RandomAction();

		} else if (decisionMode == DecisionModes.MaxUtility) {
			// temporary place for calculating utility; this could also be done separately
			UpdateUtilities();

			return GetMaximumUtilityAction();

		}
		return RandomAction();
	}

	void UpdateUtilities()
	{
		foreach (var a in agentController.actions) {
			a.utility = a.CalculateUtilityFunction(a, agentController.perception.statusParameters, stepsAhead);

		}
	}

	// gets the state with the highest current utility
	BasicAction GetMaximumUtilityAction()
	{
		float max = float.MinValue;
		BasicAction maxAction = null;
		// find maximum utility among all actions
		foreach (var a in agentController.actions) {
			if (max < a.utility) {
				max = a.utility;
				maxAction = a;
			}
		}
		if (maxAction == null) {
			return RandomAction();
		} else {
			return maxAction;
		}

	}


	// returns random action
	BasicAction RandomAction()
	{
		BasicAction actionToPerform = agentController.actions[Random.Range(0, agentController.actions.Count)];
		return actionToPerform;
	}
}