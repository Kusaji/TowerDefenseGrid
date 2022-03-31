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



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AttackRoutine()
    {
        //always true to loop indefinitely
        while (gameObject)
        {

            enemies.Clear();

            animator.SetTrigger("Attack");

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
