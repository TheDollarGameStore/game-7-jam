using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [HideInInspector] public Vector3 bulletDir;
    [SerializeField] private int damage;
    [SerializeField] private GameObject instantiateAfterHit;
    [SerializeField] private AudioClip hitSound;

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
        if (Physics.Raycast(transform.position, movement, out hit, Time.deltaTime * bulletSpeed * 1.25f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<IEnemy>().TakeDamage(damage);

                if (instantiateAfterHit != null)
                {
                    Instantiate(instantiateAfterHit, transform.position, Quaternion.identity).GetComponent<ExplosionDamage>().damage = Mathf.RoundToInt(damage / 2f);
                }

                if (hitSound != null)
                {
                    SoundManager.instance.PlayRandomized(hitSound);
                }

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
