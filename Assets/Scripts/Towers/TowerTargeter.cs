using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && target == null)
            {
                target = other.gameObject;
                targetHealth = other.gameObject.GetComponent<EnemyHealth>();
            }
            //If we already have target, compare remaining distance, attack enemy closer to end of level
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && target != null)
            {
                var newEnemy = other.gameObject.GetComponent<EnemyNavigation>();
                var currentEnemyObject = target;
                var currentEnemy = target.GetComponent<EnemyNavigation>();

                if (newEnemy.distanceLeft < currentEnemy.distanceLeft)
                {
                    target = other.gameObject;
                }
                else
                {
                    //Do nothing
                }
            }
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == false && other.gameObject == target)
            {
                target = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }
}
