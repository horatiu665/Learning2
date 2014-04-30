using UnityEngine;
using System.Collections;

public class OurCharacterController : MonoBehaviour {
		//A character's health. This variable should be protected in an actual game, as it's determined by loseHealth. As the perception-system needs to leach on all variables though, 
		//it's not worth the hassle to make custom get-functions for all private and protected variables and they are therefore set public.
	public float health = 20.0f;
		//A reference to the player that should be attacked. Placholder for system that makes more than 2 people possible. Needs to use 
	public GameObject playerToAttack;


	//the function to call when ripping the player of his health..
	public virtual void LoseHealth(GameObject attacker, float baseDamage){
		health -= baseDamage;
	}
}
