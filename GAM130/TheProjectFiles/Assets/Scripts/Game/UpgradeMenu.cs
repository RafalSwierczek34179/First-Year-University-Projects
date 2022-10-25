using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    TowerActions towerActions;
    GUIBuyUpgradeManager guiBuyScript;

    void Start()
    {
        towerActions = FindObjectOfType<TowerActions>();
        guiBuyScript = FindObjectOfType<GUIBuyUpgradeManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            towerActions.TowerUpgrade("PikeTower");
            guiBuyScript.SelectTowerToUpgrade("Pike");
        }
        else if (Input.GetKeyDown("w"))
        {
            towerActions.TowerUpgrade("FlintlockTower");
            guiBuyScript.SelectTowerToUpgrade("Flintlock");
        }
        else if (Input.GetKeyDown("e"))
        {
            towerActions.TowerUpgrade("MusketTower");
            guiBuyScript.SelectTowerToUpgrade("Musket");
        }
        else if (Input.GetKeyDown("r"))
        {
            towerActions.TowerUpgrade("MortarTower");
            guiBuyScript.SelectTowerToUpgrade("Mortar");
        }
        else if (Input.GetKeyDown("t"))
        {
            towerActions.TowerUpgrade("HalberdierTower");
            guiBuyScript.SelectTowerToUpgrade("Halbedier");
        }
        else if (Input.GetKeyDown("y"))
        {
            towerActions.TowerUpgrade("CannonTower");
            guiBuyScript.SelectTowerToUpgrade("Cannon");
        }
    }

    public void UpgradeTower()
    {
        int tower = FindObjectOfType<GUIBuyUpgradeManager>().towerToUpgrade;
        switch(tower)
        {
            case 1:
                towerActions.TowerUpgrade("PikeTower");
                guiBuyScript.SelectTowerToUpgrade("Pike");
                break;
            case 2:
                towerActions.TowerUpgrade("FlintlockTower");
                guiBuyScript.SelectTowerToUpgrade("Flintlock");
                break;
            case 3:
                towerActions.TowerUpgrade("MusketTower");
                guiBuyScript.SelectTowerToUpgrade("Musket");
                break;
            case 4:
                towerActions.TowerUpgrade("MortarTower");
                guiBuyScript.SelectTowerToUpgrade("Mortar");
                break;
            case 5:
                towerActions.TowerUpgrade("HalberdierTower");
                guiBuyScript.SelectTowerToUpgrade("Halbedier");
                break;
            case 6:
                towerActions.TowerUpgrade("CannonTower");
                guiBuyScript.SelectTowerToUpgrade("Cannon");
                break;
        }
    }
}
