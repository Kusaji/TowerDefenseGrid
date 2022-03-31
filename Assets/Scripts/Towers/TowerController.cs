using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject target;
    public GameObject towerRotationBase;

    public bool turretTrackTarget;

    public float towerAttackRange;
    public TowerTargeter towerTargeter;

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
            //towerRotationBase.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            //towerRotationBase.transform.right = (target.transform.position - transform.position);
            towerRotationBase.transform.LookAt(target.transform.position);
        }
    }
}
