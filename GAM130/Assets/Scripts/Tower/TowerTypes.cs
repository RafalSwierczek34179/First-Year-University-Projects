using UnityEngine;

public class TowerTypes
{
    public int damageValue = 1;
    public float timerLimit = 0.5f;
    public int towerCost = 1;
    public int upgrade1Cost = 1;
    public int upgrade2Cost = 1;
    public int upgrade3Cost = 1;
    public float towerRange = 1;
    public float stayingPower = 0.5f; // How long the Cannon's "beam" attack stays spawned for
    public bool isCurrentlyBuffed = false;
    public bool isRailgunTower = false;
   
    public virtual void Upgrade(GameObject tower, int currentUpgrade) { }
    public int ReturnUpgradeCost(int currentUpgrade)
    {
        switch (currentUpgrade)
        {
            case 0:
                return upgrade1Cost;
            case 1:
                return upgrade2Cost;
            case 2:
                return upgrade3Cost;
            default:
                return 0;
        }
    }
}

public class Pike : TowerTypes
{
    public Pike()
    {
        damageValue = 5;
        timerLimit = 0.25f;
        towerRange = 30;

        towerCost = 5;
        upgrade1Cost = 1;
        upgrade2Cost = 1;
        upgrade3Cost = 1;
}

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;
        GameObject rangeObject = tower.gameObject.transform.GetChild(0).gameObject;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false)
        {
            // "Increased attack speed, semi-decreased range"
            timerLimit -= 0.05f;
            towerRange -= 1;

            tower.GetComponent<TowerAttackScript>().timerLimit = timerLimit;
            rangeObject.transform.localScale = new Vector3(towerRange, 0.1f, towerRange);

            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false)
        {
            // "Chance to slow on hit"
            tower.GetComponent<TowerAttackScript>().upgradesOwned[1] = true;
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false)
        {
            // "Pike towers gain an attack speed buff based on how many on screen"
            //GameObject.Find("PlayerTowerManager").GetComponent<TowerActions>().pikeCount++;

            tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
        }
    }
}


public class Flintlock : TowerTypes
{
    public Flintlock()
    {
        damageValue = 10;
        timerLimit = 0.85f;
        towerRange = 60;

        towerCost = 10;
        upgrade1Cost = 2;
        upgrade2Cost = 2;
        upgrade3Cost = 2;
    }

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;
        GameObject rangeObject = tower.gameObject.transform.GetChild(0).gameObject;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false) //Increased attack speed
        {
            timerLimit -= 0.05f;

            tower.GetComponent<TowerAttackScript>().timerLimit = timerLimit;
            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false) //Increased range
        {
            towerRange += 5;

            rangeObject.transform.localScale = new Vector3(towerRange, 0.1f, towerRange);
            tower.GetComponent<TowerAttackScript>().upgradesOwned[1] = true;
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false) //Halved attack speed, tripled damage
        {
            timerLimit = timerLimit / 2;
            damageValue = damageValue * 3;
            
            tower.GetComponent<TowerAttackScript>().timerLimit = timerLimit;
            tower.GetComponent<TowerAttackScript>().damageValue = damageValue;
            tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
        }
    }
}

public class Musket : TowerTypes
{
    public Musket()
    {
        damageValue = 20;
        timerLimit = 1.75f;
        towerRange = 100;

        towerCost = 20;
        upgrade1Cost = 3;
        upgrade2Cost = 3;
        upgrade3Cost = 3;
    }

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false)
        {
            // Upgrade 1 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false)
        {
            // Upgrade 2 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[1] = true;
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false)
        {
            // Upgrade 3 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
        }
    }
}

public class Mortar : TowerTypes
{
    public Mortar()
    {
        damageValue = 5;
        timerLimit = 1.5f;
        towerRange = 15;

        towerCost = 40;
        upgrade1Cost = 1;
        upgrade2Cost = 1;
        upgrade3Cost = 1;
    }

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false)
        {
            // Upgrade 1 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false)
        {
            // Upgrade 2 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[1] = true;
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false)
        {
            // Upgrade 3 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
        }
    }
}

public class Halberdier : TowerTypes
{
    public Halberdier()
    {
        damageValue = 0;
        timerLimit = 1f;
        towerRange = 8.5f;

        towerCost = 55;
        upgrade1Cost = 1;
        upgrade2Cost = 1;
        upgrade3Cost = 1;
    }

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false)
        {
            // Upgrade 1 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false)
        {
            // Upgrade 2 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[1] = true;
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false)
        {
            // Upgrade 3 here
            tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
        }
    }
}

public class Cannon : TowerTypes
{
    public Cannon()
    {
        damageValue = 80;
        timerLimit = 3.5f;
        towerRange = 18f;
        isRailgunTower = true;
        stayingPower = 0.5f;

        towerCost = 80;
        upgrade1Cost = 1;
        upgrade2Cost = 1;
        upgrade3Cost = 1;
    }

    public override void Upgrade(GameObject tower, int currentUpgrade)
    {
        bool[] upgradesOwned = tower.GetComponent<TowerAttackScript>().upgradesOwned;

        if (currentUpgrade >= 1 && upgradesOwned[0] == false) //Shots stay longer
        {
            stayingPower = 1.25f; // Increase the lenght of time the shots stay
            tower.GetComponent<TowerAttackScript>().stayingPower = stayingPower;
            tower.GetComponent<TowerAttackScript>().upgradesOwned[0] = true;
        }

        if (currentUpgrade >= 2 && upgradesOwned[1] == false) //Apply slow debuff
        {

            if (tower.GetComponent<TowerAttackScript>().enemyQueue.Count > 0) // If there are enemeies in the queue
            {
                tower.GetComponent<TowerAttackScript>().enemyQueue[0].GetComponent<EnemyDebuff>().applyDebuff(1, tower); // Apply debuff 1 to the first enemy in the queue
            }
            
        }

        if (currentUpgrade >= 3 && upgradesOwned[2] == false) // Apply stun debuff
        {
            if (tower.GetComponent<TowerAttackScript>().enemyQueue.Count > 0)
            {
                tower.GetComponent<TowerAttackScript>().enemyQueue[0].GetComponent<EnemyDebuff>().applyDebuff(2, tower); 
                tower.GetComponent<TowerAttackScript>().upgradesOwned[2] = true;
            }
        }
    }
}
