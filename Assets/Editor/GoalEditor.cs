using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Goal))]
public class GoalEditor : Editor
{

	public override void OnInspectorGUI()
	{
		Goal goal = (Goal)target;

		goal.goalName = EditorGUILayout.TextField("Goal Name", goal.goalName);

		if (GUILayout.Button("Add new parameter")) {
			if (!goal.statusParameters.ContainsKey("New Parameter")) {
				goal.statusParameters.Add("New Parameter", new StatusParameter());
				goal.statusParameters["New Parameter"].value = (float)0;

			}

		}

		foreach (var sp in goal.statusParameters) {
			EditorGUILayout.TextField("Param name", sp.Key);
			sp.Value.parameterType = (ParameterTypes)EditorGUILayout.EnumPopup("Param type", sp.Value.parameterType);

			if (sp.Value.parameterType == ParameterTypes.Float) {
				EditorGUILayout.LabelField("Param value: " + (float)sp.Value.value);

			}

		}

	}

}
