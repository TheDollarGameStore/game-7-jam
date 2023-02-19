using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioClip shootSound;

    private bool loaded = true;

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
        if (Input.GetMouseButton(0) && loaded)
        {
            loaded = false;
            Invoke("Reload", 0.2f);
            Shoot();
        }
    }

    void Reload()
    {
        loaded = true;
    }
}
