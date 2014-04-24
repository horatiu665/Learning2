﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
		//A list of the mob-types that can be spawned as well as their keybindings.
	public List<MobToSpawn> thingsThatCanBeSpawned = new List<MobToSpawn>();
		//A reference to the object indicating the point at which to spawn a mob.
	public GameObject spawner;
	public Vector3 direction = Vector3.left;


	[System.Serializable]
	public class MobToSpawn {
			//The object to spawn when the appropriate input is found.
		public GameObject spawnableRef;
			//The input to spawn from.
		public KeyCode spawnKey;
		public KeyCode spawnKey2;

			//Constructor for eventual things to be defined by add new spawnerthing. Called in PlayerControlEditor on when adding to list.
		public MobToSpawn(){

		}
	}


	void Awake(){
		if(spawner == null){
			spawner = new GameObject();
			spawner.transform.position = transform.position;
		}
	}


	void Update(){
		foreach(MobToSpawn thingToSpawn in thingsThatCanBeSpawned){
			if(	Input.GetKey(thingToSpawn.spawnKey)
			   		||
			   	Input.GetKey(thingToSpawn.spawnKey2)
			){
				GameObject newMob = (GameObject) Instantiate (thingToSpawn.spawnableRef, spawner.transform.position, Quaternion.identity);
				newMob.transform.parent = GameObject.Find ("Mobmanager").transform;
			}
		}
	}
}