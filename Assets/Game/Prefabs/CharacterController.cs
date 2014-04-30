using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float health = 20.0f;


	//the function to call when ripping the player of his health..
	public virtual void loseHealth(GameObject attacker, float baseDamage){
		
		health -= baseDamage;
	}
}
