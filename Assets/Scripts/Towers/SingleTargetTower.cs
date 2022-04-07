using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetTower : MonoBehaviour
{
    [Header("Stats")]
    public int currentLevel;
    public float attacksPerSecond;
    public float attackDamageFloor;
    public float attackDamageCeiling;
    private float calculatedAPS;

    [Header("Components")]
    public TowerController controller;
    public TowerUpgrades towerUpgrades;
    public ParticleSystem shootParticles;
    private TowerTargeter towerTargeter;
    private Animator animator;
    private AudioSource speaker;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip upgradeSound;

    [Header("GameObject References")]
    public GameObject level2Visuals;
    public GameObject level3Visuals;

    // Start is called before the first frame update
    void Start()
    {
        //Setup APS
        calculatedAPS = 1 / attacksPerSecond;
        currentLevel = 1;

        animator = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();
        towerTargeter = GetComponentInChildren<TowerTargeter>();

        speaker.PlayOneShot(upgradeSound, 1.0f);

        StartCoroutine(AttackRoutine());
        StartCoroutine(CheckForUpgrades());
    }

    public void CheckUpgradeLevel()
    {
        if (towerUpgrades.currentLevel == 2 && currentLevel == 1)
        {
            attacksPerSecond++;
            calculatedAPS = 1 / attacksPerSecond;

            //Change These Values
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

            speaker.PlayOneShot(upgradeSound, 1.0f);

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
        //Add some variety to the audio.
        speaker.pitch = Random.Range(0.90f, 1.10f);
        speaker.PlayOneShot(shootSound, Random.Range(0.65f, 0.75f));

        animator.SetTrigger("Shoot");
        shootParticles.Play();
    }
}
