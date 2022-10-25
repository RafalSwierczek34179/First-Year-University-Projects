using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoolVariable))]
public class Inspector_BoolVariable : Editor
{
    public override void OnInspectorGUI()
    {
        BoolVariable targetBool = (BoolVariable)target;

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.LabelField("Initial Value: " + targetBool.InitialValue);
            EditorGUILayout.LabelField("Runtime Value: " + targetBool.RuntimeValue);
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}
