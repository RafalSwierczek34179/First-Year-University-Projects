// ExampleObject.cs

//This is the code required if we need to keep a runtime set of active "ExampleObject" scripts in the scene.

using UnityEngine;
public class ExampleObject : MonoBehaviour
{
    #region Code for a RuntimeSet
    // An object that will be managed using a RuntimeSet needs to know
    // which RuntimeSet asset(s) in the project this object will
    // belong to while active.

    [SerializeField]
    private ExampleRuntimeSet runtimeSet;

    // Advanced example... an object can belong to multiple sets
    [SerializeField]
    private ExampleRuntimeSet runtimeSet_capsule;

    public bool isCapsule = false; // Using this to optionally add an example object to a second set

    // the object adds itself to the list stored by the RuntimeSet asset whenever it is enabled....
    private void OnEnable()
    {
        runtimeSet.Add(this);

        //extra code to add this object to another runtime set...
        if(isCapsule)
        {
            runtimeSet_capsule.Add(this);
        }
    }

    //...and removes itself from the RuntimeSet whenever it is disabled.
    private void OnDisable()
    {
        runtimeSet.Remove(this);


        //extra code to remove this object to another runtime set...
        if (isCapsule)
        {
            runtimeSet_capsule.Remove(this);
        }
    }
    #endregion

    //Finally, this script will need public functions that get run on it
    public void ExampleFunction()
    {
        Debug.Log("Example Function triggered on "+gameObject.name);
    }
}