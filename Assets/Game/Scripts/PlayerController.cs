using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public List<SpawnerThing> thingsThatCanBeSpawned = new List<SpawnerThing>();

	[System.Serializable]
	public class SpawnerThing {
			//The object to spawn when the appropriate input is found.
		public GameObject spawnableRef;
			//The input to spawn from.
		public KeyCode spawnKey;
		public KeyCode spawnKey2;

			//Constructor for eventual things to be defined by add new spawnerthing. Called in PlayerControlEditor on when adding to list.
		public SpawnerThing(){

		}
	}

	void update(){
		foreach(SpawnerThing thingToSpawn in thingsThatCanBeSpawned){
			if(	Input.GetKey(thingToSpawn.spawnKey)
			   		||
			   	Input.GetKey(thingToSpawn.spawnKey2)
			){
				Instantiate (thingToSpawn.spawnableRef, transform.position, Quaternion.identity);
			}
		}
	}
}

