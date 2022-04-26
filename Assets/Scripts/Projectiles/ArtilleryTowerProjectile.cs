using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTowerProjectile : MonoBehaviour
{
    public GameObject target;

    public GameObject assignedTarget;
    public GameObject initialFlightTarget;
    public Vector3 initialFlightTargetVector;

    public float damage;
    public float initialSpeed;
    public float flightSpeed;
    public float rotationSpeed;
    
    private float currentSpeed;
    private Vector3 targetPosition;
    private float distanceToTarget;

    public bool isTriggered;
    public List<EnemyHealth> enemies;

    public GameObject explosionPrefab;
    public GameObject enemyHitEffectPrefab;
    public ArtilleryTower tower;

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
            //Vector3 relativePos = target.transform.position - transform.position;
            Vector3 relativePos = targetPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }



    void MoveTowardsTarget()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public IEnumerator DistanceToTargetRoutine()
    {
        //Set initial distance
        distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        //Loop while out of explosion range
        while (distanceToTarget > 2f)
        {
            distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            yield return new WaitForSeconds(0.1f);
        }

        Explode();
        yield break;
    }

    public IEnumerator FlightRoutine()
    {
        target = initialFlightTarget;
        initialFlightTargetVector = assignedTarget.transform.position;
        targetPosition = target.transform.position;

        currentSpeed = initialSpeed;

        yield return new WaitForSeconds(1.0f);

        target = assignedTarget;
        targetPosition = initialFlightTargetVector;

        currentSpeed = flightSpeed;
        StartCoroutine(DistanceToTargetRoutine());
    }

    public IEnumerator NewTargetFinderRoutine()
    {
        while (gameObject)
        {
            if (tower.towerTargeter.target != assignedTarget)
            {
                assignedTarget = tower.towerTargeter.target;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    /// <summary>
    /// By using a spherecast, finds all enemies in range, deals damage to them, then
    /// proceeds to remove game object and spawns explosion effects.
    /// </summary>
    public void Explode()
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
                enemy.TakeDamage(damage, gameObject);

                var hitEffect = Instantiate(enemyHitEffectPrefab, enemy.transform.position, Quaternion.identity);
                Destroy(hitEffect, 2f);
            }
        }

        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
