using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MobController : OurCharacterController {
		//Determines the movement speed of the mob.
	public float speed = 2.0f;
		//Determines the seconds between each attack
	public float attackSpeed = 2.0f;
		//Determines the raw damage a mob hits with, before mobresistances are calculated.
	public float baseDamage = 5.0f;
		//true if the mob has died and should be destroyed.
	public bool dead = false;
		//true if the mob isn't blocked from it's path and can move forward.
	private bool canMove = true;
		//the time at which the last attack occured.
	private float attacktimer = 0.0f;
		//the amount of time to wait between each attack.
	public float attackCooldown = 1.0f;
		//A list of the colliders this object's parent is hitting.
	private List<Collider> collidingObjects = new List<Collider>();

		//Attribute specifying that this List should be seen in the inspector.
	[SerializeField]
		//A list of resistances this mob has towards other mobtypes.
	public List<MobResistance> mobsresistanceToThisMob = new List<MobResistance>();
	
		//Attribute specifying that this class can be shown in the inspector.
	[System.Serializable]
		//A class containing information about the mob-type by name and the resistance.
	public class MobResistance{
			//The Mob types name
		public string key;
			//The factor by which the attacking mob's base-damage should be divided.
		public float value;
			//Contructor.
		public MobResistance(string n){
			key = n;
			value = 1.0f;
		}
	}

		//A reference to the gameObject the gameObject containing this component is child of.
	private GameObject superGameObject;

		//Initialize variables. This function is often used for this as it's called before Start() and anything in Start() will therefore have no fear of missing a reference.
	void Awake(){
			//Make a reference to the superclass 
		superGameObject = transform.parent.gameObject;
			//The superClass contains an event StartCollideWithOther. assign the function ParentOnTriggerEnter() to this event.
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StartCollideWithOther += ParentOnTriggerEnter;
			//The superClass contains an event StopCollideWithOther. assign the function ParentOnTriggerExit() to this event.
		superGameObject.transform.GetComponent<TriggerFixToBeOnParent>().StopCollideWithOther += ParentOnTriggerExit;
	}


	void Update () {
			//Call the function UpdateAwareness() to update this object's awareness veriables (dead and canMove).
		UpdateAwareness();

			//If this mob isn't dead (has run out of life)
		if(!dead){
				//Call the function move which will return false if something is in the way.
			if(!Move ()){
					//As nothing but other mobs or players can be in the way, call the attack function.
				Attack();
			}	
		}
			//If this mob is dead
		else{
				//Destroy this mob from the parent GameObject-down.
			Destroy(this.transform.parent.gameObject);
		}
	}


		/// <summary>
		/// Updates the awarenessvariables.
		/// </summary>
	void UpdateAwareness(){
			//First, if health i equals or below 0.
		if(health <= 0.0f){
				//set this object to be dead.
			dead = true;
				//If dead there is no need to finish this function. Break from it.
			return; 
		}
			//Start with canMove equals true.
		canMove = true;
		
		if(collidingObjects.Count > 0){
			//because onTriggerEnter isn't called before this, a null reference exception is called once. 
			//This is avoided by removing all colliders from the list of colliders, that has a null-reference. 
			List<Collider> destroyColliders = new List<Collider>();
				
				//foreach collider registered to touch this mob.
			foreach(Collider col in collidingObjects){
					//if the reference to the colider is valid then
				if(col != null){
						//Set a MobCollider variable to hold a reference if necessary.
					MobController asd;

						//if tha name of the gameObject colliding with is Cylinder then it must be a player, and if the parenting gameObject of the cylinder is the gameObject that we chould attack.
					if(col.transform.gameObject.name == "Cylinder"
					   &&
					   col.transform.parent.gameObject == playerToAttack){
							//set canMove to false;
						canMove = false;
					}
						//If it isn't a player
					else{
							//try to make a reference a component MobController on a child gameObject "MobChild".
						asd = col.transform.FindChild("MobChild").GetComponent<MobController>();
							
							//If getting the reference succeeded the colliding obejct must be another mob.
						if(asd != null){
								//if the colliding mob isn't an ally (targeting the same player as this mob).
							if(asd.playerToAttack != playerToAttack){
									//set canMove to false;
								canMove = false;
							}
						}
					}
				}
					//if the refence to the collider isn't valid
				else{
						//file the collider for removal once the List collidingObjects isn't iterated though any more.
					destroyColliders.Add(col);
				}
			}

				//for each collider with on valid reference found in CollidingObjects
			foreach(Collider destroyCollider in destroyColliders){
					//remove the invalid collider reference from the CollidingObjects list.
				collidingObjects.Remove (destroyCollider);
			}
		}
	}


		///<summary>Moves the mob towards the playerToAttack referenced object's position. Returns false if mob can't be moved </summary>
	private bool Move(){
			//if there is a valid reference to a gameObject to move towards && the variable canMove isn't false.
		if(playerToAttack != null
		   		&&
		   canMove){
				//make a normalized vector towards the destination.
			Vector3 direction = (playerToAttack.transform.position - superGameObject.transform.position).normalized;
				//Set this parent's rigidBody's velocity to be a vector3 of the direction vector * speed
			superGameObject.rigidbody.velocity = direction * speed;

				//This mob has moved and Move() returns true;
			return true;
		}

			//If the function makes it here the mob shouldn't be moved further and velocity in all directions is set to 0;
		superGameObject.rigidbody.velocity = Vector3.zero;
		//This mob hasn't moved and Move() returns false;
		return false;
	}

	/// <summary>
	/// Attack the first instance this mob is colliding with that isn't an ally. For a later system aggressive mobs should be prioritized over players.
	/// </summary>
	void Attack(){
			//If the cooldown for attacking has worn off.
		if(attacktimer + attackCooldown < Time.time){
				//For each of the colligind objects.
			foreach(Collider collidingObject in collidingObjects){
					//Since both MobController and PlayerController inherits a CharecterController, they can both be stored in a characterController reference
					//and the loseHealth-variable which is defined in the CharacterController can still be accessed.
				OurCharacterController asd;
					//If there is a child called "MobChild" on the colligind object's gameObject, than it's a mob.
				if(collidingObject.transform.FindChild("MobChild") != null){
						//reference MobController with the CharacterController.
					asd = collidingObject.transform.FindChild("MobChild").GetComponent<MobController>();
						//if the mob isn't an ally (this way we only attack aggressive mobs even if the CollidingObjects-list contain close allies too).
					if(asd.playerToAttack != playerToAttack){
							//attack 
						asd.LoseHealth(this.gameObject, baseDamage);
							//set cooldown for next attack.
						attacktimer = Time.time;

							//As This mob has now attacked there is no need to finish the function. Therefore break out of the foreachLoob and the function.
						return;
					}
				}
					
					//If not all of the above is true, check if the colliding object is a player.
				if(((asd = collidingObject.transform.parent.GetComponent<PlayerController>())) != null){
						//attack.
					asd.LoseHealth(this.gameObject, baseDamage);
						//Set cooldown for next attack.
					attacktimer = Time.time;
				}
			}
		}
	}

		/// <summary>
		/// Call this to make the mob lose health as defined by this override function, of the characterController.
		/// </summary>
		/// <param name="attacker">Attacker.</param>
		/// <param name="_baseDamage">_base damage.</param>
	public override void LoseHealth(GameObject attacker, float _baseDamage){
			//get the name of the attacker to reference in 
		string attackerType = attacker.transform.parent.gameObject.name;
			
			//For each of the mobtypes registered in the list of mob-types and this mob's resistances. 
		foreach(MobResistance mr in mobsresistanceToThisMob){
				//if the name of the attacker fits the name of the type.
			if(mr.key == attackerType){
					//lose health based on the resistance this mob has towards the attacking mob.
				health -= _baseDamage / mr.value;
					//if we set health here we shouldn't later in the function too. break from function to not do that.
				return;
			}
		}

			//If no resistance was found, call the overriden function to lose health as normal.
		base.LoseHealth(attacker, _baseDamage);
	}

	/// <summary>
	/// Function to be registered by the parenting gameobject's TriggerFixToBeOnParent component's custom event StartCollideWithOther.
	/// </summary>
	/// <param name="collider">Collider.</param>
	void ParentOnTriggerEnter(Collider collider){
			//if there is no collider like this one already in the list (collisions are event-specific).
		if(!collidingObjects.Exists(col => col == collider)){
				//add the collider to the list
			collidingObjects.Add(collider);
		}
	}
	
	/// <summary>
	/// Function to be registered by the parenting gameobject's TriggerFixToBeOnParent component's custom event StopCollideWithOther.
	/// </summary>
	/// <param name="collider">Collider.</param>
	void ParentOnTriggerExit(Collider collider){
			//if there is a collider like this one in the list (collisions are event-specific).
		if(collidingObjects.Exists(col => col == collider)){
				//remove the collider from the list
			collidingObjects.Remove(collider);
		}
	}
}