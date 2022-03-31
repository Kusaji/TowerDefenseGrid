using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeter : MonoBehaviour
{
    public TowerController controller;
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
        if (other.gameObject.CompareTag("Enemy") && controller.target == null)
        {
            if (other.gameObject.GetComponent<EnemyHealth>().isAlive == true)
            {
                controller.target = other.gameObject;
            }
            else
            {
                controller.target = null;
            }
        }
    }
}
