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
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
