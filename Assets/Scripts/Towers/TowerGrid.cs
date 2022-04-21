using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerGrid : MonoBehaviour
{
    public bool emptyGrid;
    public GameObject towerShopObject;
    public GameObject upgradeShopObject;
    public GameObject towerSlot;
    public List<GameObject> towerPrefabs;

    public GameObject hologramEffect;
    public GameObject purchaseEffect;

    public ShopPriceSetter shopPrice;
    public Text upgradeText;

    private TowerUpgrades currentTower;


    // Start is called before the first frame update
    void Start()
    {
        emptyGrid = true;
        hologramEffect.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShopMenu()
    {

        if (emptyGrid)
        {
            towerShopObject.SetActive(true);
            shopPrice.UpdatePrices();
        }
        else if (!emptyGrid && currentTower.currentLevel < 3)
        {
            upgradeShopObject.SetActive(true);
            upgradeText.text = $"Upgrade Cost: {currentTower.GetUpgradeCost()}";
            currentTower.GetComponent<TowerController>().towerTargeter.towerRangeMesh.enabled = true;
        }
    }

    public void CloseShopMenu()
    {
        towerShopObject.SetActive(false);
        upgradeShopObject.SetActive(false);

        if (currentTower != null)
        {
            currentTower.GetComponent<TowerController>().towerTargeter.towerRangeMesh.enabled = false;
        }
    }

    public void BuyTower(int towerPrefabNum)
    {
        var selectedTower = towerPrefabs[towerPrefabNum].gameObject;
        var selectedTowerCost = selectedTower.GetComponent<TowerUpgrades>().towerCost;

        if (emptyGrid && Economy.playerMoney >= selectedTowerCost)
        {
            Economy.playerMoney -= selectedTowerCost;

            Instantiate(
                towerPrefabs[towerPrefabNum], 
                towerSlot.transform.position, 
                Quaternion.Euler(-90f, 0f, 0f), 
                towerSlot.transform);

            emptyGrid = false;
            hologramEffect.SetActive(false);
            CloseShopMenu();

            currentTower = GetComponentInChildren<TowerUpgrades>();

            var purchaseFX = Instantiate(purchaseEffect, towerSlot.transform.position, Quaternion.Euler(-90f, 180f, 0.0f), transform);
            Destroy(purchaseFX, 2f);
        }
    }

    public void UpgradeTower()
    {
        if (Economy.playerMoney >= currentTower.GetUpgradeCost() && currentTower.currentLevel < 3)
        {
            Economy.playerMoney -= currentTower.GetUpgradeCost();
            currentTower.currentLevel++;
            CloseShopMenu();

            var purchaseFX = Instantiate(purchaseEffect, towerSlot.transform.position, Quaternion.Euler(-90f, 180f, 0.0f), transform);
            Destroy(purchaseFX, 2f);
        }
    }
}
