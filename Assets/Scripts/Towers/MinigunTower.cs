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
    public float maxAttacksPerSecond;

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
            if (towerStats.attacksPerSecond < maxAttacksPerSecond)
            {
                towerStats.attacksPerSecond += 1f;
                towerStats.CalculateAPS();
            }
            else
            {
                towerStats.attacksPerSecond = maxAttacksPerSecond;
                towerStats.CalculateAPS();
            }
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

        //If tower is targeting a "Heavy" class mob, deal double damage.
        //Encourages players to use minigun towers for levels with heavy enemy focus.
        if (towerTargeter.targetHealth.mobClass == EnemyHealth.mobType.heavy)
        {
            towerTargeter.targetHealth.TakeDamage(Damage() * 2, gameObject);
        }
        else
        {
            towerTargeter.targetHealth.TakeDamage(Damage(), gameObject);
        }
    }

    public override void Level2Upgrade()
    {
        base.Level2Upgrade();

        baseAttacksPerSecond = 8;
        maxAttacksPerSecond *= 1.50f;
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
        maxAttacksPerSecond *= 1.50f;

        towerStats.attacksPerSecond = baseAttacksPerSecond;

        towerStats.attackDamageFloor *= 1.25f;
        towerStats.attackDamageCeiling *= 1.25f;

        towerTargeter.SetTargetingRange(towerAttackRange * 1.50f);

        //towerStats.level3Visuals.SetActive(true);
        towerAudio.PlayUpgradeSound();
    }
}
