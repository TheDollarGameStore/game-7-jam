using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : IEnemy
{
    [SerializeField] private float movementSpeed;

    private bool hiding = true;

    private void Start()
    {
        base.Start();
        Invoke("HideToggle", 1.5f);
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        Vector3 moveDir = (GameManager.instance.playerObject.transform.position - transform.position).normalized;
        transform.position += moveDir * movementSpeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hiding ? 0f : 1.5f, transform.position.z), 5f * Time.deltaTime);
    }

    void HideToggle()
    {
        hiding = !hiding;
        Invoke("HideToggle", 1.5f);
    }
}
