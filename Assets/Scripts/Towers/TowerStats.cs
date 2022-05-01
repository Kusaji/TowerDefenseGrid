using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [Header("Stats")]
    public int currentLevel;
    public float attacksPerSecond;
    public float attackDamageFloor;
    public float attackDamageCeiling;
    public float calculatedAPS;

    [Header("Upgrade Costs")]
    public int towerCost;
    public int level2UpgradeCost;
    public int level3UpgradeCost;

    [Header("GameObject References")]
    public GameObject level2Visuals;
    public GameObject level3Visuals;

    [Header("Upgrade Amounts")]
    public float attackDamageUpgrade;
    public float attackRangeUpgrade;
    public float attackRateUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        CalculateAPS();

        currentLevel = 1;
    }

    public void CalculateAPS()
    {
        calculatedAPS = 1 / attacksPerSecond;
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

    public int GetUpgradeCost()
    {
        if (currentLevel == 1)
        {
            return level2UpgradeCost;
        }
        else if (currentLevel == 2)
        {
            return level3UpgradeCost;
        }
        else
        {
            return 0;
        }
    }
}
