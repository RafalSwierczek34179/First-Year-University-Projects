using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBuyUpgradeManager : MonoBehaviour
{
    enum UIStates{ Active, Inactive };

    UIStates currentUpgradeUIState;
    UIStates currentBuyUIState;

    Button toggleUpgradeMenuButton;
    Button toggleBuyMenuButton;

    enum SelectedTowerStates { Pike, Flintlock, Musket, Mortar, Halberdier, Cannon };
    SelectedTowerStates currentSelectedTowerState = SelectedTowerStates.Pike;

    public int towerToUpgrade;

    [SerializeField] GameObject highlightedTower;
    [SerializeField] GameObject buyMenu;
    [SerializeField] GameObject upgradeMenu;

    [SerializeField] GameObject upgrade1Button;
    [SerializeField] GameObject upgrade2Button;
    [SerializeField] GameObject upgrade3Button;

    [SerializeField] GameObject upgrade1OwnedObj;
    [SerializeField] GameObject upgrade2OwnedObj;
    [SerializeField] GameObject upgrade3OwnedObj;

    void Awake()
    {
        // UpgradeBackgroundImage > ToggleUpgradeMenu > Image
        toggleUpgradeMenuButton = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        currentBuyUIState = UIStates.Inactive;
        currentUpgradeUIState = UIStates.Inactive;

        SelectTowerToUpgrade("Pike");
    }
    // Active: -62.5 | Inactive: 62.5
    public void toggleGUIBuyMenu()
    {
        if (currentBuyUIState == UIStates.Inactive)
        {
            buyMenu.LeanMoveLocalY(-12.5f, 1).setEaseOutExpo().delay = 0.1f;
            currentBuyUIState = UIStates.Active;
        }
        else if (currentBuyUIState == UIStates.Active)
        {
            upgradeMenu.LeanMoveLocalY(112.5f, 1).setEaseOutExpo();
            buyMenu.LeanMoveLocalY(112.5f, 1).setEaseOutExpo();
            currentBuyUIState = UIStates.Inactive;
            currentUpgradeUIState = UIStates.Inactive;
        }
    }

    // Active: -62.5 | Inactive: 62.5
    public void toggleGUIUpgradeMenu()
    {
        if (currentUpgradeUIState == UIStates.Inactive)
        {
            buyMenu.LeanMoveLocalY(-12.5f, 1).setEaseOutExpo().delay = 0.1f;
            upgradeMenu.LeanMoveLocalY(-12.5f, 1).setEaseOutExpo().delay = 0.1f;
            currentUpgradeUIState = UIStates.Active;
            currentBuyUIState = UIStates.Active;
        }
        else if (currentUpgradeUIState == UIStates.Active)
        {
            upgradeMenu.LeanMoveLocalY(112.5f, 1).setEaseOutExpo();
            currentUpgradeUIState = UIStates.Inactive;
        }
    }

    public void SelectTowerToUpgrade(string towerType)
    {
        highlightedTower.transform.SetParent(GameObject.Find(towerType.Substring(0,4) + "UpButton").transform);
        highlightedTower.transform.localPosition = new Vector3(0,0,0);

        Text upgrade1Text = GameObject.Find("Upgrade1").transform.GetChild(0).GetComponent<Text>();
        Text upgrade2Text = GameObject.Find("Upgrade2").transform.GetChild(0).GetComponent<Text>();
        Text upgrade3Text = GameObject.Find("Upgrade3").transform.GetChild(0).GetComponent<Text>();
        TowerTypes temp;
        TowerActions towerActions = FindObjectOfType<TowerActions>();

        switch(towerType)
        {
            case "Pike":
                currentSelectedTowerState = SelectedTowerStates.Pike;
                temp = new Pike();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 1;
                break;
            case "Flintlock":
                currentSelectedTowerState = SelectedTowerStates.Flintlock;
                temp = new Flintlock();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 2;
                break;
            case "Musket":
                currentSelectedTowerState = SelectedTowerStates.Musket;
                temp = new Musket();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 3;
                break;
            case "Mortar":
                currentSelectedTowerState = SelectedTowerStates.Mortar;
                temp = new Mortar();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 4;
                break;
            case "Halberdier":
                currentSelectedTowerState = SelectedTowerStates.Halberdier;
                temp = new Halberdier();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 5;
                break;
            case "Cannon":
                currentSelectedTowerState = SelectedTowerStates.Cannon;
                temp = new Cannon();
                upgrade1Text.text = $"£{temp.upgrade1Cost}";
                upgrade2Text.text = $"£{temp.upgrade2Cost}";
                upgrade3Text.text = $"£{temp.upgrade3Cost}";
                ModifyUpgradeButtons(towerActions.currentUpgrades[towerType+"Tower"], upgrade1Text, upgrade2Text, upgrade3Text, false);
                towerToUpgrade = 6;
                break;
        }
    }

    private void ModifyUpgradeButtons(int upgradeLevel, Text upgrade1Text, Text upgrade2Text, Text upgrade3Text, bool recursive)
    {
        if (!recursive)
        {
            upgrade1Button.GetComponent<Button>().interactable = false;
            upgrade2Button.GetComponent<Button>().interactable = false;
            upgrade3Button.GetComponent<Button>().interactable = false;
            upgrade1Text.color = Color.grey;
            upgrade2Text.color = Color.grey;
            upgrade3Text.color = Color.grey;
            upgrade1OwnedObj.SetActive(false);
            upgrade2OwnedObj.SetActive(false);
            upgrade3OwnedObj.SetActive(false);
        }

        switch (upgradeLevel)
        {
            case 0:
                upgrade1Button.GetComponent<Button>().interactable = true;
                upgrade1Text.color = Color.white;
                break;
            case 1:
                upgrade1OwnedObj.SetActive(true);

                if (!recursive)
                {
                    upgrade2Button.GetComponent<Button>().interactable = true;
                    upgrade2Text.color = Color.white;
                }
                break;
            case 2:
                upgrade2OwnedObj.SetActive(true);
                ModifyUpgradeButtons(1, upgrade1Text, upgrade2Text, upgrade3Text, true);

                if (!recursive)
                {
                    upgrade3Button.GetComponent<Button>().interactable = true;
                    upgrade3Text.color = Color.white;
                }
                break;
            case 3:
                ModifyUpgradeButtons(1, upgrade1Text, upgrade2Text, upgrade3Text, true);
                ModifyUpgradeButtons(2, upgrade1Text, upgrade2Text, upgrade3Text, true);
                upgrade3OwnedObj.SetActive(true);
                break;
        }
    }
}
