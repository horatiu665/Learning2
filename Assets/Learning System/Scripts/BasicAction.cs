using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WorldState = System.Collections.Generic.Dictionary<string, StatusParameter>; // this replaces Dictionary<...> with WorldState

//A struct containing data on how much and how likely it is for a certain statusParameter to be influenced by this action.
//public class Probability
//{
//	//The name of the parameter in question.
//	public string statusParameter;
//	//The probability that the statusParameter will be changed by this action.
//	public float value;
//	//The impact at which it is likely that the statusParameter is changed by this action.
//	public float impact;

//	public void influenceProbability(float parameterImpact)
//	{
//		if (impact == 0) {
//			impact = parameterImpact;
//		} else {
//			impact *= (parameterImpact / impact) * Mathf.Sign(value);
//		}
//	}
//}


// probability needs to have a list of curves, one for each parameter in a world state.
public class Probability
{
	// this dictionary acts just like the WorldState, it uses strings (same as parameters) for indexes.
	// the x axis of curves is the impact value it will have on the parameters.
	// the y axis of curves is the probability that the corresponding value will be the right one.
	public Dictionary<string, AnimationCurve> curves;

	void AddSingleProbability(string parameter)
	{

		// adapt the curve to fit new data.

		// if a very similar case has arrived for a similar worldState, add a probability instead of making a new case.
	}

}

//An action that can be performed by the system.
public class BasicAction : MonoBehaviour
{
	[System.NonSerialized]
	public AgentController agentController;

	/// <summary>
	/// utility is used by the decision system for priorities of actions. it should be computed only once in a while if it is too complex to compute, to save computing power.
	/// </summary>
	public float utility = 0;

	//A list of the probability of impact this action has on each statusParameter.
	public List<Probability> probabilities = new List<Probability>();

	// this class is where the memory for this action is managed, and where the effects of each past action are remembered, along with potential probabilities
	public class Memory
	{
		// last times the action was performed, what was the state of the world?
		public List<WorldState> worldStates;
		// last times the action was performed, what effects did it have? what did the perception system see changed after the fact?
		public List<WorldState> effects;
		// what is the probability that these effects are likely for each state? this structure should hold info about how the action will probably affect each parameter of the worldState 
		// most basic level, a set of curves.
		public List<Probability> probabilities;
		public int memoryLimit = 10;

		// adds memory and manages amount of memories so it doesn't become too big
		public void AddMemory(WorldState worldState, WorldState effect)
		{
			worldStates.Add(worldState);
			effects.Add(effect);

			// keeps a low number of memories
			while (worldStates.Count > memoryLimit) {
				worldStates.RemoveAt(0);
				effects.RemoveAt(0);

			}

		}

		/// <summary>
		/// calculates the most similar world state when the action has been performed before. this means the effects will also be similar.
		/// </summary>
		/// <returns>returns the effect that is most likely to happen</returns>
		public WorldState EstimateEffectOn(WorldState worldState)
		{
			// calculate the nearest memory worldState using Pythagoras' theorem

			// pythagoras

			// calculate the effects of the action on the new state knowing how close it is to the old worldState

			// return the calculated effects
			//return worldStates[0];
			return worldState;

		}

	}

	// the memory of each action is used to remember old trials and their effects on the world, and then they're used in decision making.
	public Memory memory;

	public virtual IEnumerator DoAction()
	{
		yield return 0;
	}

	/// <summary>
	/// action's effect on worldState.
	/// </summary>
	/// <returns>returns the estimated effect of action on worldState</returns>
	public WorldState CalculateEffectOnWorld(BasicAction action, WorldState worldState)
	{
		// get memory of other times, compare with current world state
	//	WorldState estimatedEffect = action.memory.EstimateEffectOn(worldState);
		WorldState estimatedEffect = worldState;
		return estimatedEffect;

	}

	// this function returns the utility for this action, as specified by the different goals and the worldState argument
	public float CalculateUtilityFunction(BasicAction action, WorldState worldState, int steps = 1)
	{
		// calculate this many steps into the future
		while (steps > 0) {

			WorldState effect = CalculateEffectOnWorld(action, worldState);
			float sum = 0;
			foreach (var g in agentController.goals) {
				sum += g.Utility(effect);

			}
			// now we have the sum of utilities from each goal. this is the total utility score of this action on this world.
			
			// multiply by discount factor depending on the number of steps. the more steps, the less utility the action should have.

			steps--;
		}
		return 0;
	}


}