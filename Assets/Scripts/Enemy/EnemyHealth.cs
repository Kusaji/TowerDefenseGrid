using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth;
    public float currenthealth;
    public bool isAlive;

    public int mobValue;

    public EnemySpawner spawner;
    private AudioSource speaker;

    public GameObject deathExplosionPrefab;

    public Economy playerEconomy;


    // Start is called before the first frame update
    void Start()
    {
        //spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        speaker = GetComponent<AudioSource>();

        if (spawner.healthMultiplier > 1)
        {
            maxHealth *= spawner.healthMultiplier;
        }

        currenthealth = maxHealth;
        isAlive = true;

        speaker.pitch = Random.Range(0.80f, 1.20f);
        speaker.volume = Random.Range(0.20f, 0.30f);
        speaker.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        isAlive = false;
        spawner.enemyList.activeEnemies.Remove(gameObject);
        gameObject.tag = "Untagged";
        //Destroy agent as enemy is dead.

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = false;

        //Add money to player economy
        Economy.playerMoney += mobValue;
        playerEconomy.enemiesKilled++;

        var explosion = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);

        //Set rigidbody to kinematic and ragdoll them out of existence
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        yield return new WaitForSeconds(0.1f);
        rigidBody.AddForce(new Vector3(Random.Range(-25, 25), Random.Range(0, 10), Random.Range(-25, 25)), ForceMode.Impulse);
        rigidBody.AddTorque(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

        //Delete the gameobject
        Destroy(gameObject, 5f);
    }
}
