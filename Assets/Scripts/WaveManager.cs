using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject chaser;
    [SerializeField] private GameObject floater;
    [SerializeField] private GameObject hider;
    [SerializeField] private GameObject tank;

    [SerializeField] private GameObject weaponDrop;

    private float spawnTime = 4f;

    private void Start()
    {
        Invoke("Spawn", 2f);

        Invoke("WeaponDrop", 10f);

        Invoke("DecreaseSpawnTime", 10f);
    }

    void DecreaseSpawnTime()
    {
        spawnTime = Mathf.Max(spawnTime - 0.25f, 0.75f);

        Invoke("DecreaseSpawnTime", 15f);
    }

    void WeaponDrop()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }

        Instantiate(weaponDrop, Vector3.zero, Quaternion.identity);
        Invoke("WeaponDrop", 10f);
    }

    void Spawn()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }

        Invoke("Spawn", spawnTime);

        Vector3 spawnLocation = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 25f;

        switch (Random.Range(0, 4)) {
            case 0:
                Instantiate(chaser, spawnLocation + (Vector3.up * 1.5f), Quaternion.identity);
                break;
            case 1:
                Instantiate(floater, spawnLocation + (Vector3.up * 1f), Quaternion.identity);
                break;
            case 2:
                Instantiate(hider, spawnLocation + (Vector3.up * 1f), Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                break;
            case 3:
                Instantiate(tank, spawnLocation + (Vector3.up * 1.75f), Quaternion.identity);
                break;
        }
    }
}
