using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTower : MonoBehaviour
{
    public TowerController controller;

    public bool onCooldown;

    public float damage;
    public float attackCooldown;

    public GameObject artilleryProjectile;

    private Animator animator;
    public ParticleSystem particles;

    public TowerUpgrades towerUpgrades;
    public int currentLevel;

    public AudioClip attackSound;
    private AudioSource speaker;

    public GameObject initialFlightTarget;
    public GameObject level2Upgrade;
    public GameObject level3Upgrade;

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

            level2Upgrade.SetActive(true);

            currentLevel = towerUpgrades.currentLevel;
        }
        else if (towerUpgrades.currentLevel == 3 && currentLevel == 2)
        {
            damage *= 1.5f;
            attackCooldown -= 0.5f;

            controller.towerAttackRange += 2.5f;
            GetComponentInChildren<TowerTargeter>().SetTargetingRange(controller.towerAttackRange);

            level3Upgrade.SetActive(true);

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

        var projectile = Instantiate(artilleryProjectile, transform.position, Quaternion.identity);
        var projectileLogic = projectile.GetComponent<ArtilleryTowerProjectile>();

        projectileLogic.initialFlightTarget = initialFlightTarget;
        projectileLogic.assignedTarget = controller.target;
        projectileLogic.damage = damage;
        projectileLogic.controller = controller;

        controller.target = null;

        onCooldown = true;


        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;

    }
}
