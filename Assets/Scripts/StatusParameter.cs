﻿using UnityEngine;
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
				Value = 0;
			}
			_parameterType = value;
		}
	}
	public string name;

}
