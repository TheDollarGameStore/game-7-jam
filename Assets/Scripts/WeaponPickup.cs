using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private AudioClip weaponSwapSound;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), GameManager.instance.playerObject.transform.position) <= 1.75f)
        {
            GameManager.instance.playerObject.GetComponent<PlayerBehaviour>().SwapWeapon();
            SoundManager.instance.PlayRandomized(weaponSwapSound);
            Destroy(gameObject);
        }
    }
}
