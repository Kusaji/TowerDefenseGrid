using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : MonoBehaviour
{
    public TowerController controller;
    public bool onCooldown;
    public List<EnemyHealth> enemies;

    public float damage;
    public float attackCooldown;


    private Animator animator;
    public ParticleSystem particles;
    public GameObject enemyHitEffectPrefab;

    public TowerUpgrades towerUpgrades;
    public int currentLevel;

    public AudioClip attackSound;
    private AudioSource speaker;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();

        currentLevel = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.target != null && !onCooldown)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    public void CheckUpgradeLevel()
    {
        if (towerUpgrades.currentLevel == 2 && currentLevel == 1)
        {
            damage *= 1.5f;
            attackCooldown -= 0.5f;

            controller.towerAttackRange += 2.5f;
            GetComponentInChildren<TowerTargeter>().SetTargetingRange(controller.towerAttackRange);

            currentLevel = towerUpgrades.currentLevel;
        }
        else if (towerUpgrades.currentLevel == 3 && currentLevel == 2)
        {
            damage *= 1.5f;
            attackCooldown -= 0.5f;

            controller.towerAttackRange += 2.5f;
            GetComponentInChildren<TowerTargeter>().SetTargetingRange(controller.towerAttackRange);

            currentLevel = towerUpgrades.currentLevel;
        }
    }

    public void AttackEffects()
    {
        speaker.PlayOneShot(attackSound, 0.6f);
        animator.SetTrigger("Attack");
        particles.Play();
    }

    IEnumerator AttackRoutine()
    {

        if (currentLevel != 3)
        {
            CheckUpgradeLevel();
        }

        AttackEffects();
        onCooldown = true;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, controller.towerAttackRange, transform.forward);

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
                enemy.TakeDamage(damage);
                var hitEffect = Instantiate(enemyHitEffectPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(hitEffect, 2f);
            }
        }

        enemies.Clear();

        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;

    }
}
