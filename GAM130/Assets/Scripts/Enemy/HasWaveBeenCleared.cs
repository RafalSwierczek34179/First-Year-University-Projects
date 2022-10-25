using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasWaveBeenCleared : MonoBehaviour
{
    public Transform EnemyHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool WaveCleared()
    {
        if(this.gameObject.transform.childCount == 0 && EnemyHolder.childCount == 0)
        {
            return true;
        }
        return false;
    }
}
