using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public int waypointnum;
    public float speed;
    public float distanceLeft;

    public bool isSlowed;

    public EnemySpawner enemySpawner;
    public EnemyHealth health;
    public Economy playerEconomy;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {

        waypointnum = 0;
        agent.speed = speed;

        var spawnpos = transform.position;
        agent.Warp(spawnpos);


        target = enemySpawner.Spawnpoints[waypointnum];
        agent.SetDestination(target.transform.position);

        isSlowed = false;

        StartCoroutine(CalculateDistanceLeft());


    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator CalculateDistanceLeft()
    {
        while (health.isAlive)
        {
            distanceLeft = GetRemainingDistance();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public float GetRemainingDistance()
    {
        float distance = 0;
        Vector3[] corners = agent.path.corners;

        if (corners.Length > 2)
        {
            for (int i = 1; i < corners.Length; i++)
            {
                Vector2 previous = new Vector2(corners[i - 1].x, corners[i - 1].z);
                Vector2 current = new Vector2(corners[i].x, corners[i].z);

                distance += Vector2.Distance(previous, current);
            }
        }
        else
        {
            distance = agent.remainingDistance;
        }

        return distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject && health.isAlive)
        {
            if (waypointnum < enemySpawner.Spawnpoints.Count - 1)
            {
                waypointnum++;
                target = enemySpawner.Spawnpoints[waypointnum];
                
                agent.SetDestination(target.transform.position);
            } 
            else
            {
                playerEconomy.TakeDamage();
                GameObject.Find("ActiveEnemies").GetComponent<ActiveEnemeisList>().activeEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }


    /// <summary>
    /// Slows enemy agent.
    /// Amount is percentage based.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="duration"></param>
    public void SlowAgent(float amount, float duration)
    {
        if (health.isAlive && !isSlowed)
        {
            StartCoroutine(SlowRoutine(amount, duration));
        }
    }

    IEnumerator SlowRoutine(float amount, float duration)
    {
        var originalSpeed = speed;
        var slowedSpeed = originalSpeed * amount;
        isSlowed = true;


        speed = slowedSpeed;
        agent.speed = speed;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        agent.speed = speed;
        isSlowed = false;


    }
}
