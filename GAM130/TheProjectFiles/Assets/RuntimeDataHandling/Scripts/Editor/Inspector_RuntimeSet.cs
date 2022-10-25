//Inspector_RuntimeSet.cs
//By Paul Hedley
// A simple example of a custom editor inspector for the class "ExampleRuntimeSet"
// This can be used as a boilerplate for custom inspectors for all other runtime sets.

// Currently we would have to make a custom inspector like this for each type of data
// we want to make a RuntimeSet for.  This can potentially be overcome using the <T> generics in c#.
// ( D.R.Y! )

using UnityEditor; // All custom inspector scripts need to use this library.

[CustomEditor(typeof(ExampleRuntimeSet))] // this tells unity that this script is a custom editor and for which data type.
public class Inspector_RuntimeSet : Editor // Custom inspector scripts inherit from "Editor"
{
    //OnIspectorGUI is a special function run in the editor that draws custom inspectors.
    public override void OnInspectorGUI()
    {
        
        ExampleRuntimeSet targetSet = (ExampleRuntimeSet)target; // We need this line so that the script knows what data it is inspecting.

        // display read only information in the inspector with the EditorGUILayout.LabelField() function...

        // Display number of items in the runtime set
        EditorGUILayout.LabelField("Items in set: " + targetSet.Items.Count.ToString()); 
        // Display the name of each object in the set...
        for (int i = 0; i < targetSet.Items.Count; i++)
        {
            EditorGUILayout.LabelField(targetSet.Items[i].gameObject.name);
        }
    }
}