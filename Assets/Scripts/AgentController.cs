using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AgentController : MonoBehaviour {
		//References to the different parts of the Learning system.
	Perception perception;
	Learn learn;
	Predict predict;

		//Lists containing the  goals and actions the system can be used to achieve them.
	public List<Goal> goals = new List<Goal>();
	List<BasicAction> actions = new List<BasicAction>();
		//List containing the collected memory of the Dictionary-'statusParameters'-from-perception's states in time. 
	List<Dictionary<string,StatusParameter>> perceptionMemory = new List<Dictionary<string,StatusParameter>>();

	void Start(){
		InitializeAgent();
	}


		//The function that handles the initialization-step of the Learning system.
	void InitializeAgent(){
			//Initialize the objects needed in the system's loop.
		perception = gameObject.GetComponent<Perception>();
		perception.InitializePerception();
		learn = new Learn();
		predict = new Predict();

			//Find the actions defined in the Actions child of the agent and put them in a list for referencing.
		actions = transform.FindChild ("Actions").gameObject.GetComponents<BasicAction>().ToList();
	}


	void Update(){
		//Run through the different states in the learning system.
		perception.IteratePerception();
		learn.IterateLearn();
		predict.IteratePredict();
	}
}
