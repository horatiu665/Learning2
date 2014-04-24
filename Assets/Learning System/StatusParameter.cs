using UnityEngine;
using System.Collections;

public enum ParameterTypes
{
	Float,
	Bool
}

[System.Serializable]
public class StatusParameter {

	private object _value;
	public object Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
		}
	}
	private ParameterTypes _parameterType;
	public ParameterTypes parameterType {
		get
		{
			return _parameterType;
		}
		set
		{
			if (value == ParameterTypes.Bool) {
				Value = false;
			} else if (value == ParameterTypes.Float) {
				Value = 0.0f;
			}
			_parameterType = value;
		}
	}
	
	// initializes vars to neutral values
	public StatusParameter()
	{
		parameterType = ParameterTypes.Float;
		Value = 0.0f;
	}

	// copies the values from another instance into the new instance
	public StatusParameter(StatusParameter sp)
	{
		parameterType = sp.parameterType;
		Value = sp.Value;

	}

}
