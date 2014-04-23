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
	public List<BasicAction> actions = new List<BasicAction>();
		//List containing the collected memory of the Dictionary-'statusParameters'-from-perception's states in time. 
	public List<Dictionary<string,StatusParameter>> perceptionMemory = new List<Dictionary<string,StatusParameter>>();
		//List containing the collected memory of actions we performed over time. Will always be behind  the list of after-that-action-observed-perception memory by 1.
	public List<BasicAction> actionsMemory = new List<BasicAction>();

	void Awake(){
		InitializeAgent();
	}


		//The function that handles the initialization-step of the Learning system.
	void InitializeAgent(){
			//Initialize the objects needed in the system's loop and put reference to this class where necessary.
		perception = gameObject.GetComponentInChildren <Perception>();
		perception.agentController = this;
		perception.InitializePerception();
		learn = new Learn();
		learn.agentController = this;
		predict = new Predict();
		predict.agentController = this;

			//Find the actions defined in the Actions child of the agent and put them in a list for referencing.
		actions = transform.FindChild ("Actions").gameObject.GetComponents<BasicAction>().ToList();
	}


	void Update(){
			//Run through the different states in the learning system.
		perception.IteratePerception();
		learn.IterateLearn();
			//Choose the next action to do based on the predict class.
		BasicAction actionToDoNext = predict.IteratePredict();
			//Do the action that has been chosen to do next.
		actionToDoNext.DoAction();
			//Put the freshly done action into actionsMemory for remembrance.
		actionsMemory.Insert (0, actionToDoNext);
	}
}
