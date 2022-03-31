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

    public EnemySpawner enemySpawner;

    public EnemyHealth health;
    // Start is called before the first frame update

    private void Awake()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>(); ;
    }

    void Start()
    {

        waypointnum = 1;
        agent.speed = speed;

        var spawnpos = transform.position;
        agent.Warp(spawnpos);


        target = enemySpawner.Spawnpoints[waypointnum];
        agent.SetDestination(target.transform.position);


    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint") && health.isAlive)
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
}