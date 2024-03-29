using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    STANDARD,
    MACHINE_GUN,
    SHOTGUN,
    ROCKET
}

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject standardBulletPrefab;
    [SerializeField] private GameObject shotgunBulletPrefab;
    [SerializeField] private GameObject machineGunBulletPrefab;
    [SerializeField] private GameObject rocketBulletPrefab;

    [SerializeField] private AudioClip standardSound;
    [SerializeField] private AudioClip shotgunSound;
    [SerializeField] private AudioClip machineGunSound;
    [SerializeField] private AudioClip rocketSound;

    [SerializeField] private GameObject playerCam;

    [SerializeField] private GameObject deathParticles;

    [SerializeField] private AudioClip deathSound;

    public Weapon weapon;

    private bool loaded = true;

    void Shoot()
    {
        Bullet bulletComponent;
        switch (weapon)
        {
            case Weapon.STANDARD:
                bulletComponent = Instantiate(standardBulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bulletComponent.bulletDir = transform.rotation.eulerAngles;
                Invoke("Reload", 0.2f);
                SoundManager.instance.PlayRandomized(standardSound);
                break;
            case Weapon.SHOTGUN:
                for (int i = -4; i <= 4; i += 2)
                {
                    bulletComponent = Instantiate(shotgunBulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                    bulletComponent.bulletDir = transform.rotation.eulerAngles + new Vector3(0f, -i, 0f);
                }
                Invoke("Reload", 0.3f);
                SoundManager.instance.PlayRandomized(shotgunSound);
                break;
            case Weapon.MACHINE_GUN:
                bulletComponent = Instantiate(machineGunBulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bulletComponent.bulletDir = transform.rotation.eulerAngles + new Vector3(0f, Random.Range(-3f, 3f), 0f);
                Invoke("Reload", 0.075f);
                SoundManager.instance.PlayRandomized(machineGunSound);
                break;
            case Weapon.ROCKET:
                bulletComponent = Instantiate(rocketBulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bulletComponent.bulletDir = transform.rotation.eulerAngles;
                Invoke("Reload", 1f);
                SoundManager.instance.PlayRandomized(rocketSound);
                break;
        }

        GameManager.instance.cameraBehaviour.Nudge();
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
            Shoot();
        }
    }

    public void Die()
    {
        if (!GameManager.instance.gameOver)
        {
            SoundManager.instance.PlayRandomized(deathSound);
            playerCam.transform.parent = null;
            GameManager.instance.Lose();
            GameManager.instance.cameraBehaviour.Shake(10f);
            Instantiate(deathParticles, transform.position + (Vector3.up * 2f), Quaternion.identity);
        }
        //Destroy(gameObject);
    }

    public void SwapWeapon()
    {
        //Can't be the same weapon again.

        Weapon choice = (Weapon)Random.Range(0, 4);

        while (choice == weapon)
        {
            choice = (Weapon)Random.Range(0, 4);
        }

        CancelInvoke("Reload");
        weapon = choice;
        loaded = true;
    }

    void Reload()
    {
        loaded = true;
    }
}
