using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        if (Vector3.Distance(new Vector3(0f, 1.5f, 0f), transform.position) >= 18f)
        {
            Vector3 directionVector = transform.position - new Vector3(0f, 1.5f, 0f);

            Vector3 maxDistance = new Vector3(directionVector.x, 1.5f, directionVector.z).normalized * 18f;

            transform.position = maxDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.Lerp(direction, Vector3.zero, 5f * Time.deltaTime);
        transform.position += direction * Time.deltaTime;
    }
}
