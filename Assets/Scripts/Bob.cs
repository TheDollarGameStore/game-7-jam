using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    private Vector3 startingPos;

    private float x;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        x += speed * Time.deltaTime;

        transform.localPosition = startingPos + (direction * Mathf.Sin(x));
    }
}
