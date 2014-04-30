using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MobController : CharacterController {

	public float speed = 2.0f;
	public float attackSpeed = 2.0f;
	public float baseDamage = 5.0f;
	public bool dead = false;
	private bool canMove = true;
	private float attacktimer = 0.0f;
	public float attackCooldown = 1.0f;
	private List<Collider> collidingObjects = new List<Collider>();

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
		//
	public GameObject playerToAttack;
		//
	private GameObject superGameObject;


	void Awake(){
		superGameObject = transform.parent.gameObject;
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StartCollideWithOther += parentOnTriggerEnter;
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StopCollideWithOther += parentOnTriggerExit;
	}


	void Update () {
		UpdateAwareness();

		if(!dead){
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

			superGameObject.rigidbody.velocity = new Vector3((direction.x) * speed, 
			   	                                             (direction.y) * speed, 
			   	                                             (direction.z) * speed);
			return true;
		}

		superGameObject.rigidbody.velocity = Vector3.zero;

		return false;
	}


	void UpdateAwareness(){
		if(health <= 0.0f){
			dead = true;
		}

		canMove = true;

		if(collidingObjects.Count > 0){
				//because onTriggerEnter isn't called before this, a null reference exception is called once. 
				//This is avoided by removing all colliders from the list of colliders, that has a null-reference. 
			List<Collider> destroyColliders = new List<Collider>();

			foreach(Collider col in collidingObjects){
				if(col != null){
					MobController asd;

					if(col.transform.gameObject.name == "Cylinder"){
						canMove = false;
					}
					else{
						asd = col.transform.FindChild("MobChild").GetComponent<MobController>();

						if(asd != null){
							if(asd.playerToAttack != playerToAttack){
								canMove = false;
							}
						}
					}
				}
				else{
					destroyColliders.Add(col);
				}
			}
				
			foreach(Collider destroyCollider in destroyColliders){
				collidingObjects.Remove (destroyCollider);
			}
		}
	}


	bool Attack(){
		if(attacktimer + attackCooldown < Time.time){
			foreach(Collider collidingObject in collidingObjects){
				if(collidingObject.transform.FindChild("MobChild") != null){
					MobController asd;

					if(((asd = collidingObject.transform.FindChild("MobChild").GetComponent<MobController>())) != null){

						if(asd.playerToAttack != playerToAttack){

							asd.loseHealth(this.gameObject, baseDamage);
							attacktimer = Time.time;

							return true;
						}
					}
				}
				else if(collidingObject.transform.parent.gameObject.name == "Player"){
					PlayerController asd;

					if(((asd = collidingObject.transform.parent.GetComponent<PlayerController>())) != null){
						asd.loseHealth(this.gameObject, baseDamage);
						attacktimer = Time.time;

						return true;
					}
				}
			}
		}

		return false;
	}


	public override void loseHealth(GameObject attacker, float _baseDamage){
		string attackerType = attacker.transform.parent.gameObject.name;

		foreach(MobResistance mr in mobsresistanceToThisMob){
			if(mr.key == attackerType){
				health -= _baseDamage / mr.value;
					//if we set health here we shouldn't later in the function too. break from function to not do that.
				return;
			}
		}

		base.loseHealth(attacker, _baseDamage);
	}


	void parentOnTriggerEnter(Collider collider){
		if(!collidingObjects.Exists(col => col == collider)){
			collidingObjects.Add(collider);
		}
	}
	
	
	void parentOnTriggerExit(Collider collider){
		if(collidingObjects.Exists(col => col == collider)){
			collidingObjects.Remove(collider);
		}
	}
}