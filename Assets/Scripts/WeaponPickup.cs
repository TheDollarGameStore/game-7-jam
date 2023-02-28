using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private AudioClip weaponSwapSound;

    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

        position.Normalize();

        transform.position = (position * Random.Range(0f, 15f)) + (Vector3.up * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), GameManager.instance.playerObject.transform.position) <= 1.75f)
        {
            GameManager.instance.playerObject.GetComponent<PlayerBehaviour>().SwapWeapon();
            SoundManager.instance.PlayRandomized(weaponSwapSound);
            GameManager.instance.AddGunLevel();
            Destroy(gameObject);
        }
    }
}
