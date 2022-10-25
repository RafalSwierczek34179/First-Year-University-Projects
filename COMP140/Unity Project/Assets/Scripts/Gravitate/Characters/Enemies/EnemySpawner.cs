using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints = new GameObject[19];
    [SerializeField] private GameObject[] enemies = new GameObject[19];
    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemies[i], spawnPoints[i].transform.position, Quaternion.identity);
        }
    }
}
