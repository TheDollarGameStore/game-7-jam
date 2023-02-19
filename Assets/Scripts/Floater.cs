using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : IEnemy
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.instance.playerObject.transform.position - transform.position, Vector3.up), 0.5f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.position += transform.up * -1f * movementSpeed * Time.deltaTime;
    }
}
