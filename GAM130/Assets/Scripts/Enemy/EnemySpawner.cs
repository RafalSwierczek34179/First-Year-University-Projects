using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] listOfUniqueEnemyPrefabs;
    public Transform AttachEnemiesTo;
    public Transform spawnPoint;
    public float delayTime = 1;
    public float WavePauseTime = 10;
    public string WavesOrder;
    public string[] WaveOrders;
    int currentWave = -1;
    private int currentEnemyNumber = 0;
    bool CurrentWaveEnded = false;

    public WaveScripObject wave;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTheWave();
    }
    public void UpdateTheWave()
    {
        currentWave++;
        WavesOrder = wave.WaveText[currentWave];
        WaveOrders = WavesOrder.Split(' ');
        CurrentWaveEnded = false;
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

    public void BeginTheNulls()
    {
        WavesOrder = wave.WaveText[11];
        WaveOrders = WavesOrder.Split(' ');
        CurrentWaveEnded = false;
        StartSpawning();
    }

    IEnumerator Spawn()
    {
        int EnemyToSpawn = -1;
        /*Reads a command from Waves.txt file
         *A space needs to be between each command 
         * */
        int i = 0;
        bool keepSpawning = true;
        while (keepSpawning)
        {
            yield return new WaitForSeconds(delayTime);
            if (WaveOrders[i].Equals("Finish"))
            {
                //Debug.Log("YouWin");
                //this line should be removed if we want the game to end, but for now its fine
                i = 0;
            }
            else if(WaveOrders[i].Equals("Pause"))
            {
                Debug.Log("Pause");
                yield return new WaitForSeconds(WavePauseTime);
            }
            else if (WaveOrders[i].Equals("EndWave"))
            {
                i = i - 1;
                if(CurrentWaveEnded == false)
                {
                    this.GetComponentInParent<WavesFinished>().IncrementCounter();
                    CurrentWaveEnded = true;
                }
                StopSpawning();
                /*if (this.GetComponentInParent<WavesFinished>().IncrementCounter() == transform.parent.childCount)
                {
                    this.GetComponentInParent<WavesFinished>().StartNewWave();
                }*/

            }
            else if (WaveOrders[i].Equals("Null"))
            {
               
            }
            else if (WaveOrders[i].Equals("NullEnd"))
            {
                Debug.Log("Null");
                UpdateTheWave();
            }
            else
            {
                //Debug.Log("Spawn");
                //Debug.Log(WaveOrders[i]);
                try
                {
                    EnemyToSpawn = Int32.Parse(WaveOrders[i]);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{WaveOrders[i]}'");
                }
                GameObject newEnemy = Instantiate(listOfUniqueEnemyPrefabs[EnemyToSpawn], spawnPoint.position, Quaternion.identity);
                newEnemy.name += $"{currentEnemyNumber}";
                newEnemy.transform.parent = AttachEnemiesTo;
                currentEnemyNumber++;
            }
            i++;
        }
        

    }
}
