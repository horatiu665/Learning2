using UnityEngine;
using System.Collections;

public class ActionWait : BasicAction {

	public override IEnumerator DoAction(){
		yield return base.DoAction();
	}
}
