using System;
using UnityEngine;


[CreateAssetMenu(fileName = "RuntimeInt_NewVariable", menuName = "Runtime Variable/Integer", order = 2)]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public int InitialValue;

	[NonSerialized]
	public int RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
