using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Goal : MonoBehaviour
{
	public string goalName;

	public Dictionary<string, StatusParameter> statusParameters = new Dictionary<string,StatusParameter>();



	// Utility returns a number that signifies how important it is to fulfill this goal.
	public float Utility(Dictionary<string, StatusParameter> parameterList)
	{
		float sum = 0;
		foreach (var sp in parameterList) {
			// req.multiplier times the parameter value in the Perception system.
			if (sp.Value.parameterType == ParameterTypes.Float) {
				sum += (float)sp.Value.Value;
			}

		}
		return sum;

	}
	
}
