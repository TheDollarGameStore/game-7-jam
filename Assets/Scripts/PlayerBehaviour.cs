using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioClip shootSound;

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();

        bullet.bulletDir = transform.rotation.eulerAngles;

        GameManager.instance.cameraBehaviour.Nudge();

        SoundManager.instance.PlayRandomized(shootSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
