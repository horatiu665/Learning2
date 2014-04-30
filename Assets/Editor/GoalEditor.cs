using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

[CustomEditor(typeof(Goal))]
public class GoalEditor : Editor
{

	public override void OnInspectorGUI()
	{
		Goal goal = (Goal)target;

		goal.goalName = EditorGUILayout.TextField("Goal Name", goal.goalName);

		var list = goal.goalParameters;

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add new parameter")) {
			string n = "New Parameter";
			// if there is no goal parameter with the same name
			if (!list.Exists(gp => gp.parameterName == n)) {
				// create new goal parameter, initialize all the stuff from StatusParameter
				list.Add(new Goal.GoalParameter(n));

			}
		}

		if (GUILayout.Button("Clear all parameters")) {
			goal.goalParameters.Clear();

		}
		GUILayout.EndHorizontal();

		Goal.GoalParameter deletedParameter = null;

		foreach (var gp in goal.goalParameters) {

			// param name
			gp.parameterName = EditorGUILayout.TextField("Param name", gp.parameterName);

			//EditorGUILayout.LabelField("Parameter " + gp.parameterName);

			GUILayout.BeginHorizontal();
			// show value
			/*
			if (gp.statusParameter.parameterType == ParameterTypes.Float) {
				EditorGUILayout.LabelField("Value: " + (float)gp.statusParameter.Value);

			} else if (gp.statusParameter.parameterType == ParameterTypes.Bool) {
				EditorGUILayout.LabelField("Value: " + (bool)gp.statusParameter.Value);

			}
			*/

			// multiplier for floats
			if (gp.statusParameter.parameterType == ParameterTypes.Float) {
				gp.multiplier = EditorGUILayout.FloatField("Multiplier: ", gp.multiplier);
			}

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			// param type
			gp.statusParameter.parameterType = (ParameterTypes)EditorGUILayout.EnumPopup("Type", gp.statusParameter.parameterType);

			if (GUILayout.Button("Delete parameter")) {
				deletedParameter = gp;
				
			}

			GUILayout.EndHorizontal();

		}

		if (deletedParameter != null) {
			goal.goalParameters.Remove(deletedParameter);
		}

		if (GUI.changed) {
			EditorUtility.SetDirty(goal);
		}
	}

}
