using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [HideInInspector] public Vector3 bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(bulletDir);
        Invoke("DestroySelf", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = transform.forward * Time.deltaTime * bulletSpeed;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, movement, out hit, movement.magnitude))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<IEnemy>().TakeDamage();
                Destroy(gameObject);
            }
        }

        transform.position += movement;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
