using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetTower : MonoBehaviour
{
    public TowerController controller;
    public TowerUpgrades towerUpgrades;
    private TowerTargeter towerTargeter;


    public GameObject level2Visuals;
    public GameObject level3Visuals;

    public int currentLevel;

    public float attacksPerSecond;
    private float calculatedAPS;
    public float attackDamageFloor;
    public float attackDamageCeiling;

    public AudioClip shootSound;
    public AudioClip upgradeSound;

    public ParticleSystem shootParticles;

    private Animator animator;
    private AudioSource speaker;



    // Start is called before the first frame update
    void Start()
    {
        //Setup APS
        calculatedAPS = 1 / attacksPerSecond;
        currentLevel = 1;

        animator = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();
        towerTargeter = GetComponentInChildren<TowerTargeter>();

        speaker.PlayOneShot(upgradeSound, 0.6f);

        StartCoroutine(AttackRoutine());
        StartCoroutine(CheckForUpgrades());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckUpgradeLevel()
    {
        if (towerUpgrades.currentLevel == 2 && currentLevel == 1)
        {
            attacksPerSecond++;
            calculatedAPS = 1 / attacksPerSecond;

            attackDamageFloor *= 1.25f;
            attackDamageCeiling *= 1.25f;

            towerTargeter.SetTargetingRange(controller.towerAttackRange * 1.25f);

            level2Visuals.SetActive(true);
            speaker.PlayOneShot(upgradeSound, 0.6f);

            currentLevel = towerUpgrades.currentLevel;

        }
        else if (towerUpgrades.currentLevel == 3 && currentLevel == 2)
        {
            attacksPerSecond++;
            calculatedAPS = 1 / attacksPerSecond;

            attackDamageFloor *= 1.25f;
            attackDamageCeiling *= 1.25f;

            towerTargeter.SetTargetingRange(controller.towerAttackRange * 1.50f);

            level3Visuals.SetActive(true);

            speaker.PlayOneShot(upgradeSound, 0.6f);

            currentLevel = towerUpgrades.currentLevel;
        }
    }

    float Damage()
    {
        return Random.Range(attackDamageFloor, attackDamageCeiling);
    }

    IEnumerator AttackRoutine()
    {
        while (gameObject)
        {
            if (controller.target != null)
            {
                var enemyHealth = controller.target.GetComponent<EnemyHealth>();

                if (enemyHealth.isAlive)
                {
                    controller.target.GetComponent<EnemyHealth>().TakeDamage(Damage());
                    ShootEffects();
                    yield return new WaitForSeconds(calculatedAPS);
                }
                else
                {
                    controller.target = null;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CheckForUpgrades()
    {
        while (currentLevel < 3)
        {
            CheckUpgradeLevel();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShootEffects()
    {
        speaker.PlayOneShot(shootSound, 0.3f);
        animator.SetTrigger("Shoot");
        shootParticles.Play();
    }
}
