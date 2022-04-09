using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStartTime : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        animator.enabled = true;
    }
}
