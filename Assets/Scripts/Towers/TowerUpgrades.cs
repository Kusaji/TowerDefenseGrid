using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    public int towerCost;
    public int level2UpgradeCost;
    public int level3UpgradeCost;

    public int currentLevel;

    private void Start()
    {
        currentLevel = 1;
    }

    public void PurchaseUpgrade()
    {
        if (currentLevel == 1)
        {
            if (Economy.playerMoney >= level2UpgradeCost)
            {
                Economy.playerMoney -= level2UpgradeCost;
                currentLevel = 2;
            }
        }
        else if (currentLevel == 2)
        {
            if (Economy.playerMoney >= level3UpgradeCost)
            {
                Economy.playerMoney -= level3UpgradeCost;
                currentLevel = 3;
            }
        }
    }
}
