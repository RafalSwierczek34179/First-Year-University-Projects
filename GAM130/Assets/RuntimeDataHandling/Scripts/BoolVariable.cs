using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RuntimeBool_NewVariable", menuName = "Runtime Variable/Boolean", order = 0)]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public bool InitialValue;

	[NonSerialized]
	public bool RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
