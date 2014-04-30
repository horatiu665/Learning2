using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : CharacterController {
		//A list of the mob-types that can be spawned as well as their keybindings.
	public List<MobToSpawn> thingsThatCanBeSpawned = new List<MobToSpawn>();
		//A reference to the object indicating the point at which to spawn a mob.
	public GameObject spawner;
		//A reference to the player that should be attacked. Placholder for system that makes more than 2 people possible. Needs to use 
	public GameObject playerToAttack;


	[System.Serializable]
	public class MobToSpawn {
			//The object to spawn when the appropriate input is found.
		public GameObject spawnableRef;
			//The input to spawn from.
		public KeyCode spawnKey;
		public KeyCode spawnKey2;
		public float spawnCooldown;
		public float spawnTimer;
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
			if(thingToSpawn.spawnTimer + thingToSpawn.spawnCooldown < Time.time){
				if(	Input.GetKey(thingToSpawn.spawnKey)
				   		||
				   	Input.GetKey(thingToSpawn.spawnKey2)
				){
					GameObject newMob = (GameObject) Instantiate (thingToSpawn.spawnableRef, spawner.transform.position, Quaternion.identity);
					MobController newMobsController = newMob.transform.FindChild("MobChild").GetComponent<MobController>();
					newMobsController.playerToAttack = playerToAttack;
					//newMobsController.resistancesToMobs = thingToSpawn.spawnableRef.transform.FindChild("KnightChild").GetComponent<MobController>().resistancesToMobs;
					newMob.transform.parent = GameObject.Find ("Mobmanager").transform;
					thingToSpawn.spawnTimer = Time.time;
				}
			}
		}
	}
}