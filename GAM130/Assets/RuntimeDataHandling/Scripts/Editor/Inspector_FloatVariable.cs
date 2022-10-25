using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloatVariable))]
public class Inspector_FloatVariable : Editor
{
    public override void OnInspectorGUI()
    {
        FloatVariable targetFloat = (FloatVariable)target;

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.LabelField("Initial Value: " + targetFloat.InitialValue);
            EditorGUILayout.LabelField("Runtime Value: " + targetFloat.RuntimeValue);
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}
