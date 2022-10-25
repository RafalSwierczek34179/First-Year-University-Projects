using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TowerSelector : MonoBehaviour
{
    TowerSpawner towerSpawner;
    TowerActions towerActions;
    List<GameObject> spawnedTowers;
    int numberOfSpawnedTowers = 0;
    int currentTowerIndex = 0;

    private void Awake()
    {
        towerSpawner = this.GetComponent<TowerSpawner>();
        towerActions = this.GetComponent<TowerActions>();
    }
    private void Update()
    {
        // Resets spawnedTowers every frame so that "PopulateList()" can do its job
        spawnedTowers = new List<GameObject>();
        // Can't set spawnedTowers directly to spawnedTowerList as then making any changes to spawnedTowers will also make changes to spawnedTowerList as it'd technically be a single List
        spawnedTowers = PopulateList(towerSpawner.spawnedTowerList);
        numberOfSpawnedTowers = spawnedTowers.Count;
        
        if (numberOfSpawnedTowers != 0)
        {
            // Sorts towers in order from furthest to the left of the screen to furthest to the right
            spawnedTowers = BubbleSort(spawnedTowers);
            // Assigns the tower index to the tower
            AssignTowerIndex();
            // Checks if the player is trying to select a new tower
            CheckIfTryingToSwitchTowers();
        }
    }
    private void AssignTowerIndex()
    {
        for (int i = 0; i < spawnedTowers.Count; i++)
        {
            spawnedTowers[i].GetComponent<TowerAttackScript>().towerIndex = i;
        }
    }
    private void CheckIfTryingToSwitchTowers()
    {
        if (Input.GetButtonDown("Horizontal") && towerActions.IsATowerPickedUp() == false)
        {
            SwitchTowers((int)Input.GetAxisRaw("Horizontal"));
        }
    }

    /// <summary>
    /// Selects a tower, if selectedWithCursor is true then it'll select the tower with an index thats equal to the value of currentTower provided in the third parameter, otherwise it'll increment
    /// the currenetTowerIndex by the provided indexIncrementor and select that tower. Note: if selectedWithCursor is set to true the indexIncrementor value will be ignored so use currentTower instead.
    /// </summary>
    /// <param name="indexIncrementor"></param>
    /// <param name="isSelectedWithCursor"></param>
    /// <param name="currentTower"></param>
    public void SwitchTowers(int indexIncrementor, bool isSelectedWithCursor = false, int currentTower = -1)
    {
        UnselectTower();

        if (isSelectedWithCursor == false)
        {
            // If player clicked to the left, decrease the index, else increase index (the list stores towers in order from furthest to the left of the screen to the furthest to the right of the screen)
            currentTowerIndex += indexIncrementor;
            // Wraps the list around
            if (currentTowerIndex < 0) { currentTowerIndex = numberOfSpawnedTowers - 1; }
            else if (currentTowerIndex >= numberOfSpawnedTowers) { currentTowerIndex = 0; }
        }
        else
        {
            currentTowerIndex = currentTower;
        }

        // Selects new current tower, when a tower is selected, actions such as selling/upgrading/moving are enabled only for that specific tower
        spawnedTowers[currentTowerIndex].GetComponent<TowerAttackScript>().isSelected = true;
        HighlightCurrentTower();
    }
    public void UnselectTower()
    {
        if (numberOfSpawnedTowers > 0)
        {
            // Undo selection and any animations on the previously selected tower
            int RangeMarkersNum = spawnedTowers[currentTowerIndex].transform.childCount;
            for (int i = 0; i < RangeMarkersNum; i++)
            {
                spawnedTowers[currentTowerIndex].transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
            spawnedTowers[currentTowerIndex].GetComponent<TowerAttackScript>().isSelected = false;
        }   
    }
    private List<GameObject> PopulateList(List<GameObject> originalList)
    {
        List<GameObject> newList = new List<GameObject>();

        foreach (GameObject gameobject in originalList)
        {
            newList.Add(gameobject);
        }

        return newList;
    }
    /// <summary>
    /// Takes a list of game objects and sorts them by their x coords, at the current moment it produces a list of towers from the furthest left on the screen to the furthest right
    /// </summary>
    /// <param name="spawnedTowers"></param>
    /// <returns></returns>
    private List<GameObject> BubbleSort(List<GameObject> spawnedTowers)
    {
        bool swap = true;
        while (swap == true)
        {
            swap = false;
            for (int currentTowerIndex = 0; currentTowerIndex < spawnedTowers.Count - 1; currentTowerIndex++)
            {
                if (spawnedTowers[currentTowerIndex].transform.position.x > spawnedTowers[currentTowerIndex + 1].transform.position.x)
                {
                    GameObject tempObjectStorage = spawnedTowers[currentTowerIndex + 1].gameObject;
                    spawnedTowers[currentTowerIndex + 1] = spawnedTowers[currentTowerIndex];
                    spawnedTowers[currentTowerIndex] = tempObjectStorage;
                    swap = true;
                }
            }
        }
        return spawnedTowers;
    }
    /// <summary>
    /// Enables the towers damage zone to appear to signify to the player that its been selected and therefore can be interacted with
    /// </summary>
    private void HighlightCurrentTower()
    {
        int RangeMarkersNum = spawnedTowers[currentTowerIndex].transform.childCount;
        for (int i = 0; i < RangeMarkersNum; i++)
        {
            spawnedTowers[currentTowerIndex].transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
    }
    /// <summary>
    /// Returns the currently selected tower, if a tower is currently selected
    /// </summary>
    public GameObject SelectedTowerFinder()
    {
        GameObject towerToReturn = null;
        if (spawnedTowers != null)
        {
            foreach (GameObject tower in spawnedTowers)
            {
                if (tower.GetComponent<TowerAttackScript>().isSelected == true) { towerToReturn = tower; }
            }
        }
        return towerToReturn;
    }
    /// <summary>
    /// Decrements tower index by +/-one, should be used after a tower has been sold to avoid skipping over a tower next time the player tries to select a new tower
    /// </summary>
    public void TowerIndexValidation(int index)
    {
        currentTowerIndex -= index;
        if (currentTowerIndex < 0) { currentTowerIndex = 0; }
    }
    /// <summary>
    /// Buying a new tower that is anywhere on the left of the currently selected tower will result in the new tower being put before the selected tower in the ordered list, therefore bumping up the currently selected
    /// tower in that list up a spot. This function just increases the selected tower index by one if the new tower is to the left of the selected tower in order to reflect the bump.
    /// </summary>
    public void TowerIndexValidationForBuyingANewTower(Vector3 newTowersPosition)
    {
        // The newly bought tower is to the left of the tower the player was just on, so this adjusts the index to the right
        if (newTowersPosition == spawnedTowers[currentTowerIndex].transform.position || newTowersPosition.x <= spawnedTowers[currentTowerIndex].transform.position.x)
        {
            currentTowerIndex += 1;
            if (currentTowerIndex >= numberOfSpawnedTowers) { currentTowerIndex = 0; }

        }
        // If the new tower is to the right of the currently selected tower then nothing has to happen as the index stays in the correct place
    }
    //Enables the range markers of the selected tower
    

}
