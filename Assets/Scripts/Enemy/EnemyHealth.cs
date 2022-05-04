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
    public enum mobType { light, medium, heavy, boss};
    public mobType mobClass;

    public int mobValue;

    public EnemySpawner spawner;
    private AudioSource speaker;

    public GameObject deathExplosionPrefab;

    public Economy playerEconomy;

    public Rigidbody ragdollRb;
    public BoxCollider ragdollCollider;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        speaker = GetComponent<AudioSource>();
        animator.speed = Random.Range(0.90f, 1.10f);

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

    public void TakeDamage(float damage, GameObject damagingTurret)
    {
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            UnitDeath(damagingTurret);
        }
    }

    public void UnitDeath(GameObject damagingTurret)
    {
        isAlive = false;
        spawner.enemyList.activeEnemies.Remove(gameObject);
        gameObject.tag = "Untagged";
        //Destroy agent as enemy is dead.

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = false;



        if (ragdollCollider != null && ragdollRb != null)
        {
            ragdollRb.isKinematic = false;
            ragdollCollider.enabled = true;
        }


        //Add money to player economy
        Economy.playerMoney += mobValue;
        playerEconomy.enemiesKilled++;

        var explosion = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);

        //Set rigidbody to kinematic and ragdoll them out of existence
        //var rigidBody = GetComponent<Rigidbody>();
        //rigidBody.isKinematic = false;

        var directionOfExplosion = (transform.position - damagingTurret.transform.position).normalized;

        ragdollRb.AddForce(directionOfExplosion * Random.Range(20, 30), ForceMode.Impulse);
        ragdollRb.AddTorque(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

        //Delete the gameobject
        Destroy(gameObject, 5f);
    }

/*    IEnumerator DeathRoutine(GameObject damagingTurret)
    {
        isAlive = false;
        spawner.enemyList.activeEnemies.Remove(gameObject);
        gameObject.tag = "Untagged";
        //Destroy agent as enemy is dead.

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = false;



        if (ragdollCollider != null && ragdollRb != null)
        {
            ragdollRb.isKinematic = false;
            ragdollCollider.enabled = true;
        }


        //Add money to player economy
        Economy.playerMoney += mobValue;
        playerEconomy.enemiesKilled++;

        var explosion = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);

        //Set rigidbody to kinematic and ragdoll them out of existence
        //var rigidBody = GetComponent<Rigidbody>();
        //rigidBody.isKinematic = false;

        var directionOfExplosion = (transform.position - damagingTurret.transform.position).normalized;

        ragdollRb.AddForce(directionOfExplosion * Random.Range(10, 20), ForceMode.Impulse);
        ragdollRb.AddTorque(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

        //Delete the gameobject
        Destroy(gameObject, 5f);
    }*/
}
