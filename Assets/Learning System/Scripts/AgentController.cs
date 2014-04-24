using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AgentController : MonoBehaviour
{
	//References to the different parts of the Learning system.
	[System.NonSerialized]
	public Perception perception;
	Learn learn;
	DecisionSystem decisionSystem;

	//the amount of memory slots are available in the system.

	//Lists containing the  goals and actions the system can be used to achieve them.
	public List<Goal> goals = new List<Goal>();
	public List<BasicAction> actions = new List<BasicAction>();
	//List containing the collected memory of the Dictionary-'statusParameters'-from-perception's states in time. 
	public List<Dictionary<string, StatusParameter>> perceptionMemory = new List<Dictionary<string, StatusParameter>>();
	//List containing the collected memory of actions we performed over time. Will always be behind  the list of after-that-action-observed-perception memory by 1.
	public List<BasicAction> actionsMemory = new List<BasicAction>();
	public int actionMemoryMax = 50;

	void Awake()
	{
		InitializeAgent();
	}

	void Start()
	{
		StartCoroutine(Updater());
	}

	//The function that handles the initialization-step of the Learning system.
	void InitializeAgent()
	{
		//Initialize the objects needed in the system's loop and put reference to this class where necessary.
		perception = gameObject.GetComponentInChildren<Perception>();
		perception.agentController = this;
		perception.InitializePerception();
		learn = new Learn();
		learn.agentController = this;
		decisionSystem = new DecisionSystem();
		decisionSystem.agentController = this;

		//Find the actions defined in the Actions child of the agent and put them in a list for referencing.
		actions = transform.FindChild("Actions").gameObject.GetComponents<BasicAction>().ToList();
		// initializes the agentcontroller in each action
		actions.ForEach(a => a.agentController = this);
	}

	IEnumerator Updater()
	{
		//Run through the different states in the learning system.
		// 1. update status parameters in perception
		perception.IteratePerception();
		// 2. update action probabilities to learn from the previous step; also remember world states and effects
		learn.IterateLearn();
		// 3. Choose the next action to do based on the predict class.
		BasicAction actionToDoNext = decisionSystem.IterateDecision();
		// 4. Do the action that has been chosen to do next.
		yield return StartCoroutine(actionToDoNext.DoAction());
		// 5. Put the freshly done action into actionsMemory and remove the oldest if necessary.
		actionsMemory.Insert(0, actionToDoNext);
		while (actionsMemory.Count > actionMemoryMax) {
			actionsMemory.RemoveAt(actionsMemory.Count - 1);
		}

		StartCoroutine(Updater());
	}
}
