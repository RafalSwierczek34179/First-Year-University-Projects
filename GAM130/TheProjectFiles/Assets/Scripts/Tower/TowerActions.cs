using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerActions : MonoBehaviour
{
    private TowerSelector towerSelector;
    private MoneyManager moneyManager;
    private TowerSpawner towerSpawner;

    [SerializeField] private float sellingTowerMoneyBackMultiplier = 0.5f;
    [SerializeField] private float repositioningTowerCostMultiplier = 0.5f;

    private bool aTowerIsPickedUp = false;
    public bool ATowerIsPickedUp
    {
        get { return aTowerIsPickedUp; }
    }
    private GameObject towerToPickUpArchive;
    private bool towerCanBePlaced = false;
    [SerializeField] int towerPickUpCost = 10;
    [SerializeField] public Dictionary<string, int> currentUpgrades = new Dictionary<string, int>();
    [SerializeField] List<GameObject> listOfTowers;
    public AudioSource UpgradeSFX;
    [SerializeField] AudioSource NoMoneySound;

    private void Awake()
    {
        towerSelector = FindObjectOfType<TowerSelector>();
        moneyManager = FindObjectOfType<MoneyManager>();
        towerSpawner = this.GetComponent<TowerSpawner>();

        currentUpgrades.Add("PikeTower", 0);
        currentUpgrades.Add("FlintlockTower", 0);
        currentUpgrades.Add("MusketTower", 0);
        currentUpgrades.Add("MortarTower", 0);
        currentUpgrades.Add("HalberdierTower", 0);
        currentUpgrades.Add("CannonTower", 0);

        SetUICosts();
    }

    private void Update()
    {
        if (!towerSpawner.CurrentlyBuildingATower)
        {
            CheckIfTryingToSellTower();
            CheckIfTryingToPickUpTower();
        }
    }

    private void SetUICosts()
    {
        TowerTypes[] towerClassTypes = new TowerTypes[6] { new Pike(), new Flintlock(), new Musket(), new Mortar(), new Halberdier(), new Cannon() };
        string[] towerClassNames = new string[6] { "Pike", "Flintlock", "Musket", "Mortar", "Halberdier", "Cannon" };

        for (int i = 0; i < 6; i++)
        {
            Text towerText = GameObject.Find(towerClassNames[i] + "Button").transform.GetChild(1).GetComponent<Text>();
            towerText.text = $"£{towerClassTypes[i].towerCost}";
        }
    }

    private void CheckIfTryingToSellTower()
    {
        GameObject towerToSell = towerSelector.SelectedTowerFinder();
        if (Input.GetButtonDown("SellTower") && towerToSell != null)
        {
            // You gain 50% of the original tower price when selling it
            moneyManager.GainMoney((int)(towerToSell.GetComponent<TowerAttackScript>().towerCost * sellingTowerMoneyBackMultiplier));
            towerSpawner.DeleteASpecificTowerFromSpawnedTowerList(towerToSell);
            GameObject.Destroy(towerToSell);

            // Needs to select a new tower after the player has sold a tower
            towerSelector.SwitchTowers((int)(Input.GetAxisRaw("SellTower")));
            towerSelector.TowerIndexValidation((int)Input.GetAxisRaw("SellTower"));

            // Incase the player was currently holding the tower that they just sold
            StopCoroutine(MovingTower(towerToPickUpArchive));
            aTowerIsPickedUp = false;
        }
    }

    private void CheckIfTryingToPickUpTower()
    {
        GameObject towerToPickUp = towerSelector.SelectedTowerFinder();
        if (Input.GetButtonDown("PickUpTower") && towerToPickUp != null && aTowerIsPickedUp == false && moneyManager.CheckBalance((int)(towerToPickUp.GetComponent<TowerAttackScript>().towerCost * repositioningTowerCostMultiplier)))
        {
            towerToPickUp.layer = 2;
            // It costs 50% of the towers original value to reposition
            moneyManager.SpendMoney((int)(towerToPickUp.GetComponent<TowerAttackScript>().towerCost * repositioningTowerCostMultiplier));
            towerToPickUpArchive = towerToPickUp;
            aTowerIsPickedUp = true;
            StartCoroutine(MovingTower(towerToPickUp));
        }
        else if (Input.GetButtonDown("PickUpTower") && towerToPickUp != null && aTowerIsPickedUp == false && !moneyManager.CheckBalance((int)(towerToPickUp.GetComponent<TowerAttackScript>().towerCost * repositioningTowerCostMultiplier)) && !OptionsScript.muteSFX)
        {
            NoMoneySound.Play();
        }
        // Putting the tower down if the player is holding a tower
        else if (towerSpawner.PlayerClickedMouse1() && towerToPickUp != null && towerCanBePlaced && aTowerIsPickedUp)
        {
            Debug.Log("Player has dropped the tower");
            aTowerIsPickedUp = false;
            StopCoroutine(MovingTower(towerToPickUp));
            towerToPickUp.layer = 6;
            towerSelector.SwitchTowers(0, true, towerToPickUp.GetComponent<TowerAttackScript>().towerIndex);
        }
        // Validation
        else if(aTowerIsPickedUp && towerToPickUp == null)
        {
            aTowerIsPickedUp = false;
            StopCoroutine(MovingTower(towerToPickUpArchive));
        }
    }

    public void TowerUpgrade(string towerName)
    {
        TowerTypes towerType = null;

        switch(towerName)
        {
            case "PikeTower":
                towerType = new Pike();
                break;
            case "FlintlockTower":
                towerType = new Flintlock();
                break;
            case "MusketTower":
                towerType = new Musket();
                break;
            case "MortarTower":
                towerType = new Mortar();
                break;
            case "HalberdierTower":
                towerType = new Halberdier();
                break;
            case "CannonTower":
                towerType = new Cannon();
                break;
        }

        int upgradeCost = towerType.ReturnUpgradeCost(currentUpgrades[towerName]);

        // If there are upgrades remaining...
        if (CanUpgradeTower(currentUpgrades[towerName]) && FindObjectOfType<MoneyManager>().CheckBalance(upgradeCost))
        {
            currentUpgrades[towerName]++;
            UpgradeTowerStats(listOfTowers, currentUpgrades[towerName]);
            FindObjectOfType<MoneyManager>().SpendMoney(upgradeCost);
        }
        else
        {
            if (!OptionsScript.muteSFX) {NoMoneySound.Play(); }
        }

        FindObjectOfType<GUIBuyUpgradeManager>().SelectTowerToUpgrade(towerName);
    }

    private bool CanUpgradeTower(int num)
    {
        if (num >= 3) { return false; }
        else { return true; }
    }

    private void UpgradeTowerStats(List<GameObject> towers, int currentUpgrade)
    {
        if (OptionsScript.muteSFX == false)
        {
            UpgradeSFX.Play();
        }
            
        foreach (GameObject tower in towers)
        {
            TowerTypes towerClass = tower.GetComponent<TowerAttackScript>().currentTowerType;
            towerClass.Upgrade(tower, currentUpgrade);
        }
    }

    public int ReturnCurrentUpgradeLevel(TowerTypes towerType)
    {
        int currentUpgradeLevel = currentUpgrades[towerType.ToString()+"Tower"];
        return currentUpgradeLevel;
    }

    public bool IsATowerPickedUp()
    {
        return aTowerIsPickedUp;
    }

    IEnumerator MovingTower(GameObject tower)
    {
        var mouse = Mouse.current;
        while (aTowerIsPickedUp)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                tower.transform.position = new Vector3(hit.point.x, 1.55f, hit.point.z);
                if (hit.collider.tag == "Grass")
                {
                    towerCanBePlaced = true;
                }
                else
                {
                    towerCanBePlaced = false;
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
