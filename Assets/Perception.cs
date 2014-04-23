using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Perception: MonoBehaviour{
		
		//A dictionary of the variables used by the system's goals. the format of the corresponding update function is "Get" + the name of the variable in the dictionary.
	Dictionary<string,StatusParameter> statusParameters;

		//Fill the dictionary statusParameters with all the status parameters in the different goals at system-startup.
	public void InitializePerception(){
		statusParameters = new Dictionary<string,StatusParameter>();
	}

	///<summary>Call this class every time the perception part needs to run.</summary>
	public void IteratePerception(){

	}
}
