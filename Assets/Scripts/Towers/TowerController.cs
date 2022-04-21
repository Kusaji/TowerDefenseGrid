using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject target;
    public EnemyHealth targetHealth;
    public GameObject towerRotationBase;

    public bool turretTrackTarget;

    public float towerAttackRange;
    public TowerTargeter towerTargeter;
    public float turretTrackingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        towerTargeter.SetTargetingRange(towerAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (turretTrackTarget)
        {
            LookAtTarget();
        }
    }

    void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 relativePos = target.transform.position - towerRotationBase.transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            towerRotationBase.transform.rotation = Quaternion.Lerp(towerRotationBase.transform.rotation, toRotation, turretTrackingSpeed * Time.deltaTime);
        }
    }
}
