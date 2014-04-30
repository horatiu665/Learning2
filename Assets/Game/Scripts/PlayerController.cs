using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : OurCharacterController {
		//A list of the mob-types that can be spawned as well as their keybindings.
	public List<MobToSpawn> thingsThatCanBeSpawned = new List<MobToSpawn>();
		//A reference to the object indicating the point at which to spawn a mob.
	public GameObject spawner;

	[System.Serializable]
	public class MobToSpawn {
			//The object to spawn when the appropriate input is found.
		public GameObject spawnableRef;
			//The input to spawn from.
		public KeyCode spawnKey;
		public KeyCode spawnKey2;
			//the cooldown variable used to decide how long to wait before a new mob can be spawned.
		public float spawnCooldown;
			//timer to hold the time at which the last spawn happened.
		public float spawnTimer;
			//Variable to cheat and simulate that a key was pressed. Will not be here in a real setup.
		public bool isFalseKeyDown = false;
			//Function called to simulate a keypress.
		public void SetKeyDown(){
			isFalseKeyDown = true;
		}
			//Constructor for eventual things to be defined by add new spawnerthing. Called in PlayerControlEditor on when adding to list.
		public MobToSpawn(){

		}
	}


	void Awake(){
			//If there is no, by the inspektor, defined gameobject to spawn mobs from, create your own and place it where you are.
		if(spawner == null){
			spawner = new GameObject();
			spawner.transform.position = transform.position;
		}
	}

		//this function evaluates the key-input for all the 
	void Update(){
			//foreach instance of class MobToSpawn in the collection thingsThatCanBeSpawned
		foreach(MobToSpawn thingToSpawn in thingsThatCanBeSpawned){
				//call the function that handles if a new mob of this type should be spawned.
			checkForKeyPresses(thingToSpawn);
		}

			//if the player has run out of health
		if(health <= 0){
				//reset the level.
			Application.LoadLevel("test");
		}
	}

		//This function is called for every mobtype found in thingsToSpawn and evaluates whether the mob should be spawned based on keypresses.
	void checkForKeyPresses(MobToSpawn thingToSpawn){
			//If the cooldown has expired
		if(thingToSpawn.spawnTimer + thingToSpawn.spawnCooldown < Time.time){
				//If the right key/s are pressed.
			if(	Input.GetKey(thingToSpawn.spawnKey)
			   ||
			   Input.GetKey(thingToSpawn.spawnKey2)
			   ||
			   thingToSpawn.isFalseKeyDown
			   ){
					//Instantiate a new mob of the type in the spawnableRef at the position referenced by the spawner GameObject reference.
				GameObject newMob = (GameObject) Instantiate (thingToSpawn.spawnableRef, spawner.transform.position, Quaternion.identity);
					//For sanity purposes, shove the spawn mobs in as children of the the MobsConroller gameObject. This will make the inspector manageable during runtime.
				MobController newMobsController = newMob.transform.FindChild("MobChild").GetComponent<MobController>();
					//Set the gameObject the new mob'sintance should target.
				newMobsController.playerToAttack = playerToAttack;
					//newMobsController.resistancesToMobs = thingToSpawn.spawnableRef.transform.FindChild("KnightChild").GetComponent<MobController>().resistancesToMobs;
				newMob.transform.parent = GameObject.Find ("Mobmanager").transform;
					//set cooldown for spawning this type of mob again.
				thingToSpawn.spawnTimer = Time.time;
					//If the keypress was simulated, reset the press (can't hold down between spawn, but if pressed it will keep pressing till spawn.).
				thingToSpawn.isFalseKeyDown = false;
			}
		}
	}
}