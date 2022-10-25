using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSpawner : MonoBehaviour
{
    public GameObject[] listOfUniqueTowerPrefabs;
    public List<GameObject> spawnedTowerList = new List<GameObject>();
    public GameObject mortarExplosionPrefab;
    public GameObject cannonShotPrefab;
    public GameObject[] towerRadi;

    public AudioSource NoMoneySound;
    public AudioSource BuildSound;

    public TowerSelector animationScript;
    private TowerActions towerActions;
    private PlayerInput playerInput;
    public InputAction buildMode;
    public InputAction placeTower;

    bool currentlyBuildingATower = false;
    public bool CurrentlyBuildingATower
    {
        get { return currentlyBuildingATower; }
    }
    bool canBePlaced = false;
    int currentTowerNumber = -1;

    public TowerTypes temporaryTower;

    

    public void Start()
    {
        animationScript = FindObjectOfType<TowerSelector>();
        playerInput = GetComponent<PlayerInput>();
        towerActions = GetComponent<TowerActions>();
    }

    public void OnEnable()
    {
        buildMode.Enable();
        placeTower.Enable();
    }

    public void OnDisable()
    {
        buildMode.Disable();
        placeTower.Disable();
    }

    public void TempTower(int towerNumber)
    {
        if (towerNumber == 0)
        {
            temporaryTower = new Pike();
        }
        else if (towerNumber == 1)
        {
            temporaryTower = new Flintlock();
        }
        else if (towerNumber == 2)
        {
            temporaryTower = new Musket();
        }
        else if (towerNumber == 3)
        {
            temporaryTower = new Mortar();
        }
        else if (towerNumber == 4)
        {
            temporaryTower = new Halberdier();
        }
        else if (towerNumber == 5)
        {
            temporaryTower = new Cannon();
        }
    }
    public bool SpawnTower(int towerNumber)
    {
        if (!currentlyBuildingATower && !towerActions.ATowerIsPickedUp)
        {
            animationScript.UnselectTower();
            currentlyBuildingATower = true;
            StopCoroutine(TowerPlacement(towerNumber));
            StartCoroutine(TowerPlacement(towerNumber));
            return (true);
        }
        else if (!towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = false;
            Object.Destroy(spawnedTowerList[spawnedTowerList.Count + -1]);
            spawnedTowerList.RemoveAt(spawnedTowerList.Count + -1);
            currentTowerNumber -= 1;
            temporaryTower = null;
            return (false);
        }
        return (false);
    }

    private void Update()
    {
        CheckSelection();

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Pike();
            StopCoroutine(TowerPlacement(0));
            StartCoroutine(TowerPlacement(0));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Flintlock();
            StopCoroutine(TowerPlacement(1));
            StartCoroutine(TowerPlacement(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Musket();
            StopCoroutine(TowerPlacement(2));
            StartCoroutine(TowerPlacement(2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Mortar();
            StopCoroutine(TowerPlacement(3));
            StartCoroutine(TowerPlacement(3));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Halberdier();
            StopCoroutine(TowerPlacement(4));
            StartCoroutine(TowerPlacement(4));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && currentlyBuildingATower == false && !towerActions.ATowerIsPickedUp)
        {
            currentlyBuildingATower = true;
            temporaryTower = new Cannon();
            StopCoroutine(TowerPlacement(5));
            StartCoroutine(TowerPlacement(5));
        }
        // Hoping this works LOL just adding if currently holding the tower you can press the button again to stop holding it
        else if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6)) && currentlyBuildingATower == true)
        {
            currentlyBuildingATower = false;
            Object.Destroy(spawnedTowerList[spawnedTowerList.Count + -1]);
            spawnedTowerList.RemoveAt(spawnedTowerList.Count + -1);
            currentTowerNumber -= 1;
            temporaryTower = null;
        }
        if (placeTower.triggered && currentlyBuildingATower == true && canBePlaced && FindObjectOfType<MoneyManager>().CheckBalance(temporaryTower.towerCost)) {
            // Allows current tower to start doing damage to enemies within its range
            if (spawnedTowerList[currentTowerNumber].GetComponentInParent<TowerAttackScript>().isRailgunTower == true)
            {
                spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().isTowerSpawnedIn = true;
            }

            //Makes the building sound on place

            if (OptionsScript.muteSFX == false)
            {
                BuildSound.Play();
            }
            
            //GameObject attackRadius = spawnedTowerList[currentTowerNumber].transform.GetChild(0).gameObject;
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().isTowerSpawnedIn = true;

            // Allows current tower to start detecting enemies withing its range
            spawnedTowerList[currentTowerNumber].GetComponentInChildren<TowerDetectEnemy>().DetectEnemiesOnSpawn();

            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().damageValue = temporaryTower.damageValue;
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().timerLimit = temporaryTower.timerLimit;
            //attackRadius.transform.localScale = new Vector3(temporaryTower.towerRange, 0.4f, temporaryTower.towerRange); //This line caused the cannon tower's radius to break, so I've commented it out
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().isCurrentlyBuffed = temporaryTower.isCurrentlyBuffed;
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().isRailgunTower = temporaryTower.isRailgunTower;
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().towerCost = temporaryTower.towerCost;
            spawnedTowerList[currentTowerNumber].GetComponent<TowerAttackScript>().currentTowerType = temporaryTower;

            // Gives this tower the upgrades.
            temporaryTower.Upgrade(spawnedTowerList[currentTowerNumber], FindObjectOfType<TowerActions>().currentUpgrades[spawnedTowerList[currentTowerNumber].name.Replace("(Clone)", "")]);
            //Makes the range invisible on spawn
            int RangeMarkersNum = spawnedTowerList[currentTowerNumber].transform.childCount;
            for (int i = 0; i < RangeMarkersNum; i++)
            {
                Debug.Log("test");
                spawnedTowerList[currentTowerNumber].transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
            FindObjectOfType<MoneyManager>().SpendMoney(temporaryTower.towerCost);
            currentlyBuildingATower = false;
            spawnedTowerList[spawnedTowerList.Count + -1].layer = 6;

            // Fixes index issues when a new tower is bought
            this.GetComponent<TowerSelector>().TowerIndexValidationForBuyingANewTower(spawnedTowerList[currentTowerNumber].transform.position);
        }
        else if (placeTower.triggered && currentlyBuildingATower == true && canBePlaced && !FindObjectOfType<MoneyManager>().CheckBalance(temporaryTower.towerCost))
        {

            if (OptionsScript.muteSFX == false)
            {
                NoMoneySound.Play();
            }
        }
    }

    public void DeleteASpecificTowerFromSpawnedTowerList(GameObject towerObject)
    {
        for (int towerIndex = 0; towerIndex < spawnedTowerList.Count; towerIndex++)
        {
            if (spawnedTowerList[towerIndex] == towerObject)
            {
                spawnedTowerList.RemoveAt(towerIndex);
                currentTowerNumber -= 1;
            }
        }
    }

    IEnumerator TowerPlacement(int towerNumber)
    {
        var mouse = Mouse.current;

        currentTowerNumber++;
        spawnedTowerList.Add(Instantiate(listOfUniqueTowerPrefabs[towerNumber], new Vector3(3, 1.55f, 2), Quaternion.identity));
        while (currentlyBuildingATower)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

            if (Physics.Raycast(ray, out hit))
            {
                spawnedTowerList[currentTowerNumber].transform.position = new Vector3(hit.point.x, 1.55f, hit.point.z);
                if (hit.collider.CompareTag("Grass"))
                {
                    canBePlaced = true;
                }
                else
                {
                    canBePlaced = false;
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    void CheckSelection()
    {
        var mouse = Mouse.current;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tower") && hit.collider.GetComponent<TowerAttackScript>().isTowerSpawnedIn && placeTower.triggered && !towerActions.IsATowerPickedUp())
            {
                
                animationScript.SwitchTowers(0, true, hit.collider.GetComponent<TowerAttackScript>().towerIndex);
            }
            else if (!hit.collider.CompareTag("Tower") && placeTower.triggered && !towerActions.IsATowerPickedUp() && !currentlyBuildingATower)
            {
                animationScript.GetComponent<TowerSelector>().UnselectTower();
            }
        }
    }
    public bool PlayerClickedMouse1()
    {
        return placeTower.triggered;
    }
}
