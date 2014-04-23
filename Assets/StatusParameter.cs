using UnityEngine;
using System.Collections;

public enum ParameterTypes
{
	Float,
	Bool
}

[System.Serializable]
public class StatusParameter {

	public object value;
	public ParameterTypes parameterType;

}
