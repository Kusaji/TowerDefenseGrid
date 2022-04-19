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

    public bool isSlowed;

    public EnemySpawner enemySpawner;

    public EnemyHealth health;
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


    }

    // Update is called once per frame
    void Update()
    {
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
                //subtract from players lives / health
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
