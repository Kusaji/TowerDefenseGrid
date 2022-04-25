using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunTower : Tower
{
    [Header("Tower Specific Objects")]
    public GameObject currentTarget;

    public int baseAttacksPerSecond;
    public int level2AttacksPerSecond;
    public int level3AttacksPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        base.StartCoroutine(AttackRoutine());
        towerTargeter.SetTargetingRange(towerAttackRange);

        baseAttacksPerSecond = 6;
        towerStats.attacksPerSecond = baseAttacksPerSecond;

    }

    private void Update()
    {
        if (towerTrackTarget)
        {
            base.LookAtTarget();
        }

        if (currentTarget != null && towerTargeter.target == null)
        {
            towerStats.attacksPerSecond = baseAttacksPerSecond;
            currentTarget = null;
            towerStats.CalculateAPS();
        }
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
    }

    public override void Attack()
    {
        base.Attack();

        if (currentTarget == null)
        {
            currentTarget = towerTargeter.target;
        }
        else if (currentTarget == towerTargeter.target)
        {
            towerStats.attacksPerSecond += 0.25f;
            towerStats.CalculateAPS();
        }
        else if (currentTarget != towerTargeter.target)
        {
            currentTarget = towerTargeter.target;
            towerStats.attacksPerSecond = baseAttacksPerSecond;
            towerStats.CalculateAPS();
        } 
        else if (towerTargeter.target == null)
        {
            towerStats.attacksPerSecond = baseAttacksPerSecond;
            currentTarget = null;
            towerStats.CalculateAPS();
        }

        towerTargeter.targetHealth.TakeDamage(Damage());
    }

    public override void Level2Upgrade()
    {
        base.Level2Upgrade();

        baseAttacksPerSecond = 8;
        towerStats.attacksPerSecond = baseAttacksPerSecond;

        towerStats.attackDamageFloor *= 1.25f;
        towerStats.attackDamageCeiling *= 1.25f;

        towerTargeter.SetTargetingRange(towerAttackRange * 1.25f);

        //towerStats.level2Visuals.SetActive(true);
        towerAudio.PlayUpgradeSound();
    }

    public override void Level3Upgrade()
    {
        base.Level3Upgrade();

        baseAttacksPerSecond = 10;
        towerStats.attacksPerSecond = baseAttacksPerSecond;

        towerStats.attackDamageFloor *= 1.25f;
        towerStats.attackDamageCeiling *= 1.25f;

        towerTargeter.SetTargetingRange(towerAttackRange * 1.50f);

        //towerStats.level3Visuals.SetActive(true);
        towerAudio.PlayUpgradeSound();
    }
}
