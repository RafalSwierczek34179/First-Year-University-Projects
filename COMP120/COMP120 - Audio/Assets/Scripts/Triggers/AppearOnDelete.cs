using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnDelete : MonoBehaviour
{
    public GameObject gameObjectInstance;

    // Update is called once per frame
    void Update()
    {
        if (gameObjectInstance.gameObject == null)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponent<DeleteOnDistance>().enabled = true;
        }
    }
}
