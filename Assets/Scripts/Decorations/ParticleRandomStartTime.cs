using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRandomStartTime : MonoBehaviour
{
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        particles.Play();
    }
}
