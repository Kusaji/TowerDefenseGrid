using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTower : Tower
{
    [Header("Attack Stats")]
    public bool onCooldown;
    public float slowPercentage;
    public float slowDuration;
    public float attackCooldown;
    
    [Header("Enemies")]
    public List<EnemyNavigation> enemies;

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

    public override void Attack()
    {
        base.Attack();
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
    }

    public override void Level2Upgrade()
    {
        base.Level2Upgrade();

        slowDuration *= 1.5f;
        slowPercentage -= 0.05f;
        attackCooldown -= 1;

        towerAudio.PlayUpgradeSound();
    }

    public override void Level3Upgrade()
    {
        base.Level3Upgrade();

        slowDuration *= 1.5f;
        slowPercentage -= 0.05f;
        attackCooldown -= 1;

        towerAudio.PlayUpgradeSound();
    }

    public new IEnumerator AttackRoutine()
    {
        //Clear enemy list per attack
        enemies.Clear();
        onCooldown = true;

        Attack(); //Used for Effects / sounds

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, towerAttackRange, transform.forward);

        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                enemies.Add(hit.transform.gameObject.GetComponent<EnemyNavigation>());
            }
        }

        if (enemies.Count > 0)
        {
            foreach (EnemyNavigation enemy in enemies)
            {
                enemy.SlowAgent(slowPercentage, slowDuration);
            }
        }

        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;
    }
}
