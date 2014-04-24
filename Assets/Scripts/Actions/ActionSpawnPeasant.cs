using UnityEngine;
using System.Collections;

public class ActionSpawnPeasant : BasicAction {

	public override IEnumerator DoAction(){
		yield return base.DoAction();
	}
}
