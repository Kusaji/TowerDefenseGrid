using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPriceSetter : MonoBehaviour
{
    public TowerGrid towerGrid;

    public Text basicTower;
    public Text minigunTower;
    public Text slowingTower;
    public Text aoeTower;
    public Text artilleryTower;

    // Start is called before the first frame update


    public void UpdatePrices()
    {
        basicTower.text = $"Basic Tower \n ${towerGrid.towerPrefabs[0].GetComponent<Tower>().towerStats.towerCost}";
        slowingTower.text = $"Slowing Tower \n ${towerGrid.towerPrefabs[1].GetComponent<Tower>().towerStats.towerCost}";
        aoeTower.text = $"AOE Tower \n ${towerGrid.towerPrefabs[2].GetComponent<Tower>().towerStats.towerCost}";
        artilleryTower.text = $"Missile Tower \n ${towerGrid.towerPrefabs[3].GetComponent<Tower>().towerStats.towerCost}";
        minigunTower.text = $"Minigun Tower \n ${towerGrid.towerPrefabs[4].GetComponent<Tower>().towerStats.towerCost}";
    }
}
