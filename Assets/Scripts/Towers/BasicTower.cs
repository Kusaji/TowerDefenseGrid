using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    public Tower tower;

    // Start is called before the first frame update
    void Start()
    {
        base.StartCoroutine(AttackRoutine());
        towerTargeter.SetTargetingRange(towerAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
       if (towerTrackTarget)
        {
            base.LookAtTarget();
        } 
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
    }

    public override void Attack()
    {
        base.Attack();
        towerTargeter.targetHealth.TakeDamage(tower.Damage());
    }

    public override void Level2Upgrade()
    {
        base.Level2Upgrade();

        towerStats.attacksPerSecond++;
        towerStats.calculatedAPS = 1 / towerStats.attacksPerSecond;

        //Change These Values
        towerStats.attackDamageFloor *= 1.25f;
        towerStats.attackDamageCeiling *= 1.25f;

        towerTargeter.SetTargetingRange(towerAttackRange * 1.25f);

        towerStats.level2Visuals.SetActive(true);
        towerAudio.PlayUpgradeSound();
    }

    public override void Level3Upgrade()
    {
        base.Level3Upgrade();
        towerStats.attacksPerSecond++;
        towerStats.calculatedAPS = 1 / towerStats.attacksPerSecond;

        towerStats.attackDamageFloor *= 1.25f;
        towerStats.attackDamageCeiling *= 1.25f;

        towerTargeter.SetTargetingRange(towerAttackRange * 1.50f);

        towerStats.level3Visuals.SetActive(true);
        towerAudio.PlayUpgradeSound();
    }
}
