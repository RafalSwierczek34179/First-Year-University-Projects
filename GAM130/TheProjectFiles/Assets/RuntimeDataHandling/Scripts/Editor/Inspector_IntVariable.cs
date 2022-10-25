using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IntVariable))]
public class Inspector_IntVariable : Editor
{
    public override void OnInspectorGUI()
    {
        IntVariable targetInt = (IntVariable)target;

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.LabelField("Initial Value: " + targetInt.InitialValue);
            EditorGUILayout.LabelField("Runtime Value: " + targetInt.RuntimeValue);
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}

