using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MobController : MonoBehaviour {

	public float speed = 2.0f;
	public float health = 20.0f;
	public float baseDamage = 5.0f;
	public bool dead = false;
	private bool canMove = true;
	private float attacktimer = 0.0f;
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
	
	public GameObject playerToAttack;
	private GameObject superGameObject;


	void Awake(){
		superGameObject = transform.parent.gameObject;
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StartCollideWithOther += parentOnTriggerEnter;
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StopCollideWithOther += parentOnTriggerExit;
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
		MobController asd;
		if((asd = collidingObjects[0].GetComponent<MobController>()) != null){
			asd.loseHealth(this.gameObject);
			
			return true;
		}

		return false;
	}


	public void loseHealth(GameObject attacker){
		string attackerType = attacker.name;
		foreach(MobResistance mr in mobsresistanceToThisMob){
			if(mr.key == attackerType){
				health -= baseDamage / mr.value;
				return;
			}
		}

		health -= baseDamage;
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