using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRandomTowerSpawner : MonoBehaviour
{
    public List<GameObject> towers;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(towers[Random.Range(0, towers.Count)], transform.position, Quaternion.Euler(-90f, 0f, 0f), transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
