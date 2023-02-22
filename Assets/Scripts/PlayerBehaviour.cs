using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioClip shootSound;

    [SerializeField] private GameObject playerCam;

    [SerializeField] private GameObject deathParticles;

    [SerializeField] private AudioClip deathSound;

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
        if (GameManager.instance.gameOver)
        {
            return;
        }

        if (Input.GetMouseButton(0) && loaded)
        {
            loaded = false;
            Invoke("Reload", 0.2f);
            Shoot();
        }
    }

    public void Die()
    {
        if (!GameManager.instance.gameOver)
        {
            SoundManager.instance.PlayRandomized(deathSound);
            playerCam.transform.parent = null;
            GameManager.instance.gameOver = true;
            GameManager.instance.cameraBehaviour.Shake(10f);
            Instantiate(deathParticles, transform.position + (Vector3.up * 2f), Quaternion.identity);
        }
        //Destroy(gameObject);
    }

    void Reload()
    {
        loaded = true;
    }
}
