using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacker : MonoBehaviour
{
    public TowerController controller;

    public float attacksPerSecond;
    public float attackDamageFloor;
    public float attackDamageCeiling;


    // Start is called before the first frame update
    void Start()
    {
        //Setup APS
        attacksPerSecond = 1 / attacksPerSecond;

        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    yield return new WaitForSeconds(attacksPerSecond);
                }
                else
                {
                    controller.target = null;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
