using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeter : MonoBehaviour
{
    public GameObject target;
    public EnemyHealth targetHealth;

    public bool targetNewestEnemy;
    public MeshRenderer towerRangeMesh;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!targetHealth.isAlive)
            {
                target = null;
                targetHealth = null;
            }
        }
    }

    public void SetTargetingRange(float range)
    {
        var newRange = new Vector3(range, transform.localScale.y, range);
        transform.localScale = newRange;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //If nothing is targeted at all
            if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && target == null && targetHealth == null)
            {
                target = other.gameObject;
                targetHealth = other.gameObject.GetComponent<EnemyHealth>();
            }

            //If we already have target, compare remaining distance, attack enemy closer to end of level
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && target != null && targetHealth != null)
            {
                var currentEnemy = target.GetComponent<EnemyNavigation>();
                var newEnemy = other.gameObject.GetComponent<EnemyNavigation>();
                

                if (newEnemy.distanceLeft < currentEnemy.distanceLeft)
                {
                    target = other.gameObject;
                    targetHealth = other.gameObject.GetComponent<EnemyHealth>();
                }
                else
                {
                    //Do nothing
                }
            }
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == false && other.gameObject == target)
            {
                target = null;
                targetHealth = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
            targetHealth = null;
        }
    }
}
