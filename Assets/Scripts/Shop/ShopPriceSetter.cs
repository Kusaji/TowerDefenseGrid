using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPriceSetter : MonoBehaviour
{
    public TowerGrid towerGrid;

    public Text basicTower;
    public Text slowingTower;

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
    }
}
