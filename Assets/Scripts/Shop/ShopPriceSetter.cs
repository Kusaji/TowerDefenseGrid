using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPriceSetter : MonoBehaviour
{
    public TowerGrid towerGrid;

    public Text basicTower;
    public Text slowingTower;
    public Text aoeTower;
    public Text artilleryTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePrices()
    {
        basicTower.text = $"Basic Tower \n ${towerGrid.towerPrefabs[0].GetComponent<TowerUpgrades>().towerCost}";
        slowingTower.text = $"Slowing Tower \n ${towerGrid.towerPrefabs[1].GetComponent<TowerUpgrades>().towerCost}";
        aoeTower.text = $"AOE Tower \n ${towerGrid.towerPrefabs[2].GetComponent<TowerUpgrades>().towerCost}";
        artilleryTower.text = $"Artillery Tower \n ${towerGrid.towerPrefabs[3].GetComponent<TowerUpgrades>().towerCost}";
    }
}
