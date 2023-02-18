using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : IEnemy
{
    [SerializeField] private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.instance.playerObject.transform.position - transform.position, Vector3.up), 5f * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
