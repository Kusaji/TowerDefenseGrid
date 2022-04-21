using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerTargeter : MonoBehaviour
{
    public TowerController controller;
    public bool targetNewestEnemy;


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

    //Retarget each new enemy
    //OnTriggerEnter

    //Lock On
    /*    private void OnTriggerStay(Collider other)
        {
            if (!targetNewestEnemy)
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true)
                    {
                        var newEnemy = other.gameObject;
                        controller.target = other.gameObject;
                        enemies.Add(other.gameObject.GetComponent<EnemyNavigation>().distanceLeft);
                        //enemies.Sort();
                    }
                    else
                    {
                        controller.target = null;
                    }
                }
            }
        }*/

    //Always shoot newest target
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //If nothing is targeted at all
            if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && controller.target == null)
            {
                controller.target = other.gameObject;
            }
            //If we already have target, compare remaining distance, attack enemy closer to end of level
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true && controller.target != null)
            {
                var newEnemy = other.gameObject.GetComponent<EnemyNavigation>();
                var currentEnemyObject = controller.target;
                var currentEnemy = controller.target.GetComponent<EnemyNavigation>();

                if (newEnemy.distanceLeft < currentEnemy.distanceLeft)
                {
                    controller.target = other.gameObject;
                }
                else
                {
                    //Do nothing
                }
            }
            else if (other.gameObject.GetComponent<EnemyHealth>().isAlive == false && other.gameObject == controller.target)
            {
                controller.target = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == controller.target)
        {
            controller.target = null;
        }
    }
}
