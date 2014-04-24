using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Goal : MonoBehaviour
{
	public string goalName;

	// serializable so that the changes are saved between plays (unity needs this setting) 
	[System.Serializable]
	public class GoalParameter
	{
		// this is only used when setting up the Perception system
		public string parameterName;
		// when setting up the Perception system, new statusParameters are created in there and this will be a reference to the correct one.
		public StatusParameter statusParameter;
		public float multiplier;

		public GoalParameter(string name)
		{
			parameterName = name;
			multiplier = 1;
			statusParameter = new StatusParameter();
		}
	}

	// this is not a dictionary because it is impossible to change the key in a dictionary, and the key acts as the parameter name so it might need to be changed sometimes.
	public List<GoalParameter> goalParameters = new List<GoalParameter>();


	//This function MUST be u(x) = s*x, where s is a real number, because of the way the action effects are estimated (by combining all possible effects and so on)
	public float Utility(Dictionary<string, StatusParameter> parameterList)
	{
		float sum = 0;
		// each status parameter in goalParameters is found in parameterList and added to the sum
		foreach (var gp in goalParameters) {
			if (parameterList.ContainsValue(gp.statusParameter)) {
				if (gp.statusParameter.parameterType == ParameterTypes.Float) {
					// value times the multiplier of said parameter represent the utility of that parameter for this goal
					sum += gp.multiplier * (float)gp.statusParameter.Value;
				}
			}

		}

		return sum;
	}
	
}
