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
		plCtl.health = (float)EditorGUILayout.FloatField("Health", plCtl.health);
		plCtl.spawner = (GameObject)EditorGUILayout.ObjectField("spawner", (Object)plCtl.spawner, typeof(GameObject), true);
		plCtl.playerToAttack = (GameObject)EditorGUILayout.ObjectField("Player to attack", (Object)plCtl.playerToAttack, typeof(GameObject), true);

		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

		var list = plCtl.thingsThatCanBeSpawned;


		GUILayout.BeginHorizontal();


		if (GUILayout.Button("Add new mobtype")) {
				// Make a new spawnerthing to store the new mob in.
			list.Add(new PlayerController.MobToSpawn());
		}
		
		if (GUILayout.Button("Clear all mobtypes")) {
				// Clear all mobs.
			plCtl.thingsThatCanBeSpawned.Clear();
		}


		GUILayout.EndHorizontal();


		PlayerController.MobToSpawn deletedMob = null;
		
		foreach (var pt in plCtl.thingsThatCanBeSpawned) {
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});


			GUILayout.BeginHorizontal();


			pt.spawnableRef = (GameObject)EditorGUILayout.ObjectField("Spawnable Ref", (Object)pt.spawnableRef, typeof(GameObject), true);


			GUILayout.EndHorizontal();


			GUILayout.BeginHorizontal();


			pt.spawnKey = (KeyCode)EditorGUILayout.EnumPopup("Main keybind for spawn", pt.spawnKey);
			pt.spawnKey2 = (KeyCode)EditorGUILayout.EnumPopup("Alt keybind for spawn", pt.spawnKey2);


			GUILayout.EndHorizontal();


			if (GUILayout.Button("Delete mobtype")) {
				deletedMob = pt;
			}
		}

		if (deletedMob != null) {
			plCtl.thingsThatCanBeSpawned.Remove(deletedMob);
		}
		
		if (GUI.changed) {
			EditorUtility.SetDirty(plCtl);
		}
	}
	
}