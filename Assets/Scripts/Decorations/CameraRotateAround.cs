using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
    public GameObject lookAtObject;
    public Vector3 offset;
    public Vector3 lookAtPosition;
    public float rotationSpeed;
    public List<float> rotationSpeeds;

    public float rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
        lookAtPosition = lookAtObject.transform.position + offset;
        rotationSpeed = rotationSpeeds[Random.Range(0, rotationSpeeds.Count)];

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtPosition);
        transform.RotateAround(lookAtPosition, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
