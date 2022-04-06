using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    public bool emptyGrid;
    public GameObject towerShopObject;
    public GameObject towerSlot;
    public List<GameObject> towerPrefabs;

    public GameObject hologramEffect;


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
        }
    }

    public void CloseShopMenu()
    {
        towerShopObject.SetActive(false);
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
        }
    }

}
