using UnityEngine;
using System.Collections;

public enum ParameterTypes
{
	Float,
	Bool
}

/// <summary>
/// The value is absolute for the parameters when they are referred to as worldState, and relative when they are used as action effects.
/// </summary>
[System.Serializable]
public class StatusParameter {

	private object _value;
	// absolute for world state, relative for action effect
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
