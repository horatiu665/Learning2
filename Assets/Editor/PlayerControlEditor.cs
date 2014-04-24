using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

[CustomEditor(typeof(PlayerController))]
public class PlayerControlEditor : Editor
{
	
	public override void OnInspectorGUI()
	{
		PlayerController plCtl = (PlayerController)target;
		
		var list = plCtl.thingsThatCanBeSpawned;
		
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add new mobtype")) {
			// if there is no goal parameter with the same name
			list.Add(new PlayerController.SpawnerThing());
			/*if (!list.Exists(gp => gp.parameterName == n)) {
				// create new goal parameter, initialize all the stuff from StatusParameter

				
			}*/
			
		}
		
		if (GUILayout.Button("Clear all mobtypes")) {
			plCtl.thingsThatCanBeSpawned.Clear();
			
		}

		GUILayout.EndHorizontal();
		
		PlayerController.SpawnerThing deletedParameter = null;
		
		foreach (var pt in plCtl.thingsThatCanBeSpawned) {
			
			// param name
			//pt.parameterName = EditorGUILayout.TextField("Param name", pt.parameterName);
			
			//EditorGUILayout.LabelField("Parameter " + gp.parameterName);
			
			GUILayout.BeginHorizontal();
			EditorGUILayout.ObjectField("Spawnable Ref", (Object)pt.spawnableRef, typeof(GameObject), false);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			pt.spawnKey = (KeyCode)EditorGUILayout.EnumPopup("Main keybind for spawn", pt.spawnKey);
			pt.spawnKey2 = (KeyCode)EditorGUILayout.EnumPopup("Alt keybind for spawn", pt.spawnKey2);
			// show value
			/*
			if (gp.statusParameter.parameterType == ParameterTypes.Float) {
				EditorGUILayout.LabelField("Value: " + (float)gp.statusParameter.Value);

			} else if (gp.statusParameter.parameterType == ParameterTypes.Bool) {
				EditorGUILayout.LabelField("Value: " + (bool)gp.statusParameter.Value);

			}
			*/
			
			// multiplier for floats
		/*	if (pt.statusParameter.parameterType == ParameterTypes.Float) {
				pt.multiplier = EditorGUILayout.FloatField("Multiplier: ", pt.multiplier);
			}
		*/	
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			// param type
			//pt.statusParameter.parameterType = (ParameterTypes)EditorGUILayout.EnumPopup("Type", pt.statusParameter.parameterType);
			
			if (GUILayout.Button("Delete parameter")) {
				deletedParameter = pt;
				
			}
			
			GUILayout.EndHorizontal();
			
		}
		
		if (deletedParameter != null) {
			plCtl.thingsThatCanBeSpawned.Remove(deletedParameter);
		}
		
		if (GUI.changed) {
			EditorUtility.SetDirty(plCtl);
		}
	}
	
}