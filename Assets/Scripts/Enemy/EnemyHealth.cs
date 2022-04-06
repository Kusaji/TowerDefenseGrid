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


    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        isAlive = true;
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
        //Destroy agent as enemy is dead.

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = false;

        //Add money to player economy
        Economy.playerMoney += mobValue;

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
