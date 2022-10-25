using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSpawner : MonoBehaviour
{
    public GameObject[] prefab;

    public void Spawn()
    {
        Instantiate(prefab[Random.Range(0,prefab.Length)]);
    }
}
