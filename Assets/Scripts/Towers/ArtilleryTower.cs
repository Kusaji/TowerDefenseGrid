using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTower : Tower
{
    [Header("Attack Stats")]
    public bool onCooldown;
    public float attackCooldown;
    public float damage;

    [Header("Tower Specific Prefabs")]
    public GameObject artilleryProjectile;
    public GameObject initialFlightTarget;


    // Start is called before the first frame update
    void Start()
    {
        towerTargeter.SetTargetingRange(towerAttackRange);

    }

    // Update is called once per frame
    void Update()
    {
        if (towerTargeter.target != null && !onCooldown)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    public override void Level2Upgrade()
    {
        base.Level2Upgrade();

        towerStats.attackDamageFloor *= 1.5f;
        towerStats.attackDamageCeiling *= 1.5f;

        attackCooldown -= 0.5f;

        towerAttackRange += 2.5f;
        towerTargeter.SetTargetingRange(towerAttackRange);

        //towerStats.level2Visuals.SetActive(true);
    }

    public override void Level3Upgrade()
    {
        base.Level3Upgrade();

        towerStats.attackDamageFloor *= 1.5f;
        towerStats.attackDamageCeiling *= 1.5f;

        attackCooldown -= 0.5f;

        towerAttackRange += 2.5f;
        towerTargeter.SetTargetingRange(towerAttackRange);

        towerStats.level2Visuals.SetActive(true);
    }


    public new IEnumerator AttackRoutine()
    {
        var projectile = Instantiate(artilleryProjectile, transform.position, Quaternion.identity);
        var projectileLogic = projectile.GetComponent<ArtilleryTowerProjectile>();

        base.Attack();

        projectileLogic.initialFlightTarget = initialFlightTarget;
        projectileLogic.assignedTarget = towerTargeter.target;
        projectileLogic.damage = damage;
        projectileLogic.tower = this;
        onCooldown = true;

        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;

    }
}
