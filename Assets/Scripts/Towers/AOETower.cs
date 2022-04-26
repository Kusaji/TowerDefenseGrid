using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : Tower
{
    [Header("Attack Stats")]
    public bool onCooldown;
    public float attackCooldown;

    [Header("Enemies")]
    public List<EnemyHealth> enemies;

    [Header("Effect Prefabs")]
    public GameObject enemyHitEffectPrefab;

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
    }

    public override void Level3Upgrade()
    {
        base.Level3Upgrade();

        towerStats.attackDamageFloor *= 1.5f;
        towerStats.attackDamageCeiling *= 1.5f;
        attackCooldown -= 0.5f;

        towerAttackRange += 2.5f;
        towerTargeter.SetTargetingRange(towerAttackRange);
    }

    public new IEnumerator AttackRoutine()
    {
        onCooldown = true;
        base.Attack();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, towerAttackRange, transform.forward);

        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                var enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();

                if (enemyHealth.isAlive)
                {
                    enemies.Add(hit.transform.GetComponent<EnemyHealth>());
                }
            }
        }

        if (enemies.Count > 0)
        {
            foreach (EnemyHealth enemy in enemies)
            {
                enemy.TakeDamage(base.Damage(), gameObject);
                var hitEffect = Instantiate(enemyHitEffectPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(hitEffect, 2f);
            }
        }

        enemies.Clear();

        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;

    }
}
