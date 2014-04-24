using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobController : MonoBehaviour {

	public float speed = 2.0f;
	public float health = 20.0f;
	public float baseDamage = 5.0f;
	private bool dead = false;
	private bool canMove = true;
	private List<Collision> collidingObjects = new List<Collision>();

	[SerializeField]
	public List<MobResistance> mobsresistanceToThisMob = new List<MobResistance>();
	

	[System.Serializable]
	public class MobResistance{
		public string key;
		public float value;

		public MobResistance(string n){
			key = n;
		}
	}


	public GameObject playerToAttack;
	private GameObject superGameObject;

	void Awake(){
		superGameObject = transform.parent.gameObject;
	}


	void Update () {
		if(!dead){
			UpdateAwareness();

			if(!Move ()){
				Attack();
			}	
		}
		else{
			Destroy(this.transform.parent.gameObject);
		}
	}


	bool Move(){
		if(playerToAttack != null
		   		&&
		   canMove){
			Vector3 direction = (playerToAttack.transform.position - superGameObject.transform.position).normalized;

			if(canMove){
				superGameObject.rigidbody.velocity = new Vector3((direction.x) * speed, 
			    	                                             (direction.y) * speed, 
			    	                                             (direction.z) * speed);
				return true;
			}
			else{
				superGameObject.rigidbody.velocity = Vector3.zero;

				return false;
			}
		}

		return false;
	}


	void UpdateAwareness(){
		if(collidingObjects.Count > 0){
			canMove = false;
		}
		else{
			canMove = true;
		}
	}


	bool Attack(){

		return true;
	}

	void OnCollisionEnter(Collision collision){
		print ("asds");
		if(!collidingObjects.Exists (col => col == collision)){
			collidingObjects.Add(collision);
		}
	}

	void OnCollisionExit(Collision collision){
		if(collidingObjects.Exists(col => col == collision)){
			collidingObjects.Remove(collision);
		}
	}
}