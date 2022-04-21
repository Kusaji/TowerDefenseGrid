using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDecorationMover : MonoBehaviour
{
    public float moveSpeed;

    public Vector3 startingPosition;
    public Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        StartCoroutine(PickNewLocation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    IEnumerator PickNewLocation()
    {
        while (gameObject)
        {
            newPosition = new Vector3(
                startingPosition.x + Random.Range(-3, 3),
                startingPosition.y + Random.Range(-2, 2),
                startingPosition.z + Random.Range(-3, 3));

            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }
}
