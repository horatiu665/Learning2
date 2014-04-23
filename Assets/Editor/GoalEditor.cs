using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Goal))]
public class GoalEditor : Editor
{

	string newParamName = "";

	public override void OnInspectorGUI()
	{
		Goal goal = (Goal)target;

		goal.goalName = EditorGUILayout.TextField("Goal Name", goal.goalName);

		newParamName = EditorGUILayout.TextField("New parameter name", newParamName);
		if (GUILayout.Button("Add new parameter")) {
			string n = newParamName;
			if (!goal.statusParameters.ContainsKey(n)) {
				goal.statusParameters.Add(n, new StatusParameter());

			}

		}

		if (GUILayout.Button("Clear all parameters")) {
			goal.statusParameters.Clear();

		}

		foreach (var sp in goal.statusParameters) {

			// param name
			//EditorGUILayout.TextField("Param name", sp.Key);


			if (sp.Value.parameterType == ParameterTypes.Float) {
				EditorGUILayout.LabelField("Parameter " + sp.Key + ": " + (float)sp.Value.Value);

			} else if (sp.Value.parameterType == ParameterTypes.Bool) {
				EditorGUILayout.LabelField("Parameter " + sp.Key + ": " + (bool)sp.Value.Value);


			}

			// param type
			sp.Value.parameterType = (ParameterTypes)EditorGUILayout.EnumPopup("Type", sp.Value.parameterType);

		}

	}

}
