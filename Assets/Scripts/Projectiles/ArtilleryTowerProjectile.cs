using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTowerProjectile : MonoBehaviour
{
    public GameObject target;

    public GameObject assignedTarget;
    public GameObject initialFlightTarget;
    private Vector3 targetPosition;

    public float damage;
    public float initialSpeed;
    public float flightSpeed;
    private float currentSpeed;

    public bool isTriggered;
    public List<EnemyHealth> enemies;

    public GameObject explosionPrefab;
    public GameObject enemyHitEffectPrefab;
    public TowerController controller;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlightRoutine());
        isTriggered = false;
        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        MoveTowardsTarget();
    }

    void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 7f * Time.deltaTime);
        }
    }



    void MoveTowardsTarget()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public IEnumerator FlightRoutine()
    {
        target = initialFlightTarget;
        currentSpeed = initialSpeed;

        yield return new WaitForSeconds(1.0f);

        target = assignedTarget;
        currentSpeed = flightSpeed;
        StartCoroutine(NewTargetFinderRoutine());
    }

    public IEnumerator NewTargetFinderRoutine()
    {
        while (gameObject)
        {
            if (controller.target != assignedTarget)
            {
                assignedTarget = controller.target;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isTriggered)
        {
            isTriggered = true;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 8f, transform.forward);

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

            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(gameObject);
        }
    }
}
