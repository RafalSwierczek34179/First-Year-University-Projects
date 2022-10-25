using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWaveStartScript : MonoBehaviour
{
    public GameObject Button;
    public GameObject Enemies;

    public void StartWave()
    {
        WaveNumberScript.waveNumber = 1;
        Button.SetActive(false);
        Enemies.SetActive(true);

    }
}
