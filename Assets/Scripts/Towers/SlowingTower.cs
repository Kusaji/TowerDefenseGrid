using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTower : MonoBehaviour
{
    public TowerController controller;
    public List<EnemyNavigation> enemies;

    public float slowPercentage;
    public float slowDuration;

    public float attackCooldown;

    private Animator animator;
    public ParticleSystem particles;

    public TowerUpgrades towerUpgrades;
    public int currentLevel;

    public AudioClip attackSound;
    private AudioSource speaker;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();

        StartCoroutine(AttackRoutine());
        currentLevel = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckUpgradeLevel()
    {
        if (towerUpgrades.currentLevel == 2 && currentLevel == 1)
        {
            slowDuration *= 1.5f;
            slowPercentage *= 1.25f;
            attackCooldown -= 1;
            currentLevel = towerUpgrades.currentLevel;
        }
        else if (towerUpgrades.currentLevel == 3 && currentLevel == 2)
        {
            slowDuration *= 1.5f;
            slowPercentage *= 1.25f;
            attackCooldown -= 1;
            currentLevel = towerUpgrades.currentLevel;
        }
    }

    public void AttackEffects()
    {
        speaker.PlayOneShot(attackSound, 0.4f);
        animator.SetTrigger("Attack");
        particles.Play();
    }

    IEnumerator AttackRoutine()
    {
        //always true to loop indefinitely
        while (gameObject)
        {
            if (currentLevel != 3)
            {
                CheckUpgradeLevel();
            }
            enemies.Clear();

            AttackEffects();

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, controller.towerAttackRange, transform.forward);

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

        }
    }
}
