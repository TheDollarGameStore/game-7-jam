using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject chaser;
    [SerializeField] private GameObject floater;
    [SerializeField] private GameObject hider;

    private float spawnTime = 4f;

    private void Start()
    {
        Invoke("Spawn", 2f);

        Invoke("DecreaseSpawnTime", 10f);
    }

    void DecreaseSpawnTime()
    {
        spawnTime = Mathf.Max(spawnTime - 0.25f, 0.5f);

        Invoke("DecreaseSpawnTime", 10f);
    }

    void Spawn()
    {
        Invoke("Spawn", spawnTime);

        Vector3 spawnLocation = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 25f;

        switch (Random.Range(0, 3)) {
            case 0:
                Instantiate(chaser, spawnLocation + (Vector3.up * 1.5f), Quaternion.identity);
                break;
            case 1:
                Instantiate(floater, spawnLocation + (Vector3.up * 1f), Quaternion.identity);
                break;
            case 2:
                Instantiate(hider, spawnLocation + (Vector3.up * 1f), Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                break;
        }
    }
}
