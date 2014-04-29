using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(MobController))]
public class MobControlEditor : Editor
{
	
	public override void OnInspectorGUI()
	{
		MobController mlCtl = (MobController)target;
		mlCtl.speed = (float)EditorGUILayout.FloatField("Speed", mlCtl.speed);
		mlCtl.health = (float)EditorGUILayout.FloatField("Health", mlCtl.health);
		mlCtl.baseDamage = (float)EditorGUILayout.FloatField("Base Damage", mlCtl.baseDamage);
		mlCtl.attackSpeed = (float)EditorGUILayout.FloatField("Attack Speed", mlCtl.attackSpeed);

		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
		
		var list = mlCtl.mobsresistanceToThisMob;
		
		
		GUILayout.BeginHorizontal();
		
		
		if (GUILayout.Button("Add new resilientMob")) {
			string n = "New resistance";
			// if there is no resistance stored with the same name
			if (!list.Exists(mr => mr.key == n)) {
				// create new goal parameter, initialize all the stuff from StatusParameter
				list.Add(new MobController.MobResistance(n));
				
			}
		}
		
		if (GUILayout.Button("Clear all resilientMob")) {
			// Clear all resistances.
			mlCtl.mobsresistanceToThisMob.Clear();
		}
		
		
		GUILayout.EndHorizontal();
		
		
		MobController.MobResistance deletedresistance = null;
	//	string[] keys = mlCtl.resistancesToMobs.Keys.ToArray();

		foreach (MobController.MobResistance ml in mlCtl.mobsresistanceToThisMob) {
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

			
			GUILayout.BeginHorizontal();


			ml.key = (string)EditorGUILayout.TextField (ml.key);
			ml.value = (float)EditorGUILayout.FloatField(ml.value);

			
			GUILayout.EndHorizontal();
			
			
			if (GUILayout.Button("Delete resistance")) {
				deletedresistance = ml;
			}
		}
		
		if (deletedresistance != null) {
			mlCtl.mobsresistanceToThisMob.Remove(deletedresistance);
		}
		
		if (GUI.changed) {
			EditorUtility.SetDirty(mlCtl);
		}
	}
	
}