using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Vector3 rotation;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
