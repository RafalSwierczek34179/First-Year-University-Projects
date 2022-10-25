using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    TowerSpawner Thingy;

    void Start()
    {
         Thingy = FindObjectOfType<TowerSpawner>();
    }

    void Update()
    {
        
    }

    public void Spawn0()
    {
        if (Thingy.SpawnTower(0))
        {
            Thingy.TempTower(0);
        }
    }
    public void Spawn1()
    {
        if (Thingy.SpawnTower(1))
        {
            Thingy.TempTower(1);
        }
    }

    public void Spawn2()
    {
        if (Thingy.SpawnTower(2))
        {
            Thingy.TempTower(2);
        }
    }

    public void Spawn3()
    {
        if (Thingy.SpawnTower(3))
        {
            Thingy.TempTower(3);
        }
    }

    public void Spawn4()
    {
        if (Thingy.SpawnTower(4))
        {
            Thingy.TempTower(4);
        }
    }

    public void Spawn5()
    {
        if (Thingy.SpawnTower(5))
        {
            Thingy.TempTower(5);
        }
    }

}
