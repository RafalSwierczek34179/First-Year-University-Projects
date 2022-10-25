using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public float delayTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawn());

        StartSpawning();
    }

    public void StartSpawning()
    {
        StopCoroutine(Spawn());
        StartCoroutine(Spawn());
    }

    public void StopSpawning()
    {
        StopCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
