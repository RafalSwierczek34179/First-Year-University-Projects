using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesFinished : MonoBehaviour
{
    [SerializeField] int WavesCompleted = 0;
    [SerializeField] List<GameObject> SpawnLocations;
    [SerializeField] GameObject Enemies;
    [SerializeField] GameObject WaveOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            SpawnLocations.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(WavesCompleted == SpawnLocations.Count && Enemies.transform.childCount == 0)
        {
            WaveOverScreen.SetActive(true);
            Time.timeScale = 0f;
            StartNewWave();
        }
    }
    public void StartNewWave()
    {
        WavesCompleted = 0;
        foreach(GameObject child in SpawnLocations)
        {
            child.GetComponentInChildren<EnemySpawner>().BeginTheNulls();
        }
        WaveNumberScript.waveNumber += 1;
    }
    public void IncrementCounter()
    {
        WavesCompleted++;
    }
}
