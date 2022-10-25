//ExampleController.cs
//example script for any object or logic in the game
// to control a RuntimeSet
using UnityEngine;

public class ExampleController : MonoBehaviour
{
        //Any script that controls a runtime set will need a
        //// reference to the RuntimeSet asset in the project
        [SerializeField]
        private ExampleRuntimeSet set_allObjects;


    // Advanced example - If the controller knows about other runtime sets, it can act upon them too if required.
    [SerializeField]
    private ExampleRuntimeSet set_capsuleObjects;

    //An example function that runs that runs through each script in the RuntimeSet
    //// and runs a function on it
    public void RunFunctionOnAll()
        {
            for (int i = 0; i < set_allObjects.Items.Count; i++)
            {
                set_allObjects.Items[i].ExampleFunction();
            }   
        }


    // Extra example function so that this script can run a function on a different set it knows about...
    public void RunFunctionOnCapsules()
    {
        for (int i = 0; i < set_capsuleObjects.Items.Count; i++)
        {
            set_capsuleObjects.Items[i].ExampleFunction();
        }
    }


    // A debug script that dispays how many items are in the runtime set.
    public void ShowCount()
        {
            if(!set_allObjects)
            {
                Debug.LogError("You need to assign an ExampleRuntimeSet asset to the ExampleController script on "+gameObject.name);
                return;
            }
            Debug.Log("There are " + set_allObjects.Items.Count + " example objects.");
        }
}
