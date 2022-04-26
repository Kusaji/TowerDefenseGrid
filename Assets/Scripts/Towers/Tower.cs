using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all towers to derive from.
/// </summary>
public class Tower : MonoBehaviour
{
    [Header("Game Objects")]
    public TowerTargeter towerTargeter;
    public GameObject towerRotationBase; //Needed for correcting model rotation

    [Header("Options")]
    public bool towerTrackTarget; //Does the tower track targets?
    public float towerTrackingSpeed; //Cosmetic
    public float towerAttackRange;

    [Header("Components For Other Scripts")]
    public AudioSource speaker;
    public Animator animator;
    public TowerStats towerStats;
    public TowerAudio towerAudio;
    public ParticleSystem attackParticles;

    private Vector3 lookAtOffset = new Vector3(0.0f, 0.35f, 0.0f);


    /// <summary>
    /// Used so towers don't "Snap" to targets, and intead smoothly look / follow targets.
    /// </summary>
    public void LookAtTarget()
    {
        if (towerTargeter.target != null)
        {
            Vector3 relativePos = (towerTargeter.target.transform.position + lookAtOffset) - towerRotationBase.transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            towerRotationBase.transform.rotation = Quaternion.Lerp(towerRotationBase.transform.rotation, toRotation, towerTrackingSpeed * Time.deltaTime);
        }
    }

    #region Attack
    public virtual void Attack()
    {
        animator.SetTrigger("Shoot");
        attackParticles.Play();
        towerAudio.PlayAttackSound();
    }

    /// <summary>
    /// Returns a float from a specified range for damage dealt to enemy.
    /// </summary>
    /// <returns></returns>
    public float Damage()
    {
        return Random.Range(towerStats.attackDamageFloor, towerStats.attackDamageCeiling);
    }

    public IEnumerator AttackRoutine()
    {
        while (gameObject)
        {
            if (towerTargeter.target != null)
            {
                if (towerTargeter.targetHealth.isAlive)
                {
                    Attack();
                    yield return new WaitForSeconds(towerStats.calculatedAPS);
                }
                else
                {
                    //Do nothing
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion


    #region Upgrades
    public virtual void UpgradeTower()
    {
        if (towerStats.currentLevel == 1)
        {
            towerStats.currentLevel = 2;
            Level2Upgrade();
        }
        else if (towerStats.currentLevel == 2)
        {
            towerStats.currentLevel = 3;
            Level2Upgrade();
        }
    }

    public virtual void Level2Upgrade()
    {

    }

    public virtual void Level3Upgrade()
    {

    }
    #endregion
}
