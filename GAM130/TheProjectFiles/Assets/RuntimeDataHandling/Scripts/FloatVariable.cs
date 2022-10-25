using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RuntimeFloat_NewVariable", menuName = "Runtime Variable/Float", order = 1)]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
