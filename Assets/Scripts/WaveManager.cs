using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject chaser;
    [SerializeField] private GameObject floater;

    private void Start()
    {
        Invoke("Spawn", 2f);
    }

    void Spawn()
    {
        Invoke("Spawn", 4f);

        Vector3 spawnLocation = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 25f;

        switch (Random.Range(0, 2)) {
            case 0:
                Instantiate(chaser, spawnLocation + (Vector3.up * 1.5f), Quaternion.identity);
                break;
            case 1:
                Instantiate(floater, spawnLocation + (Vector3.up * 1f), Quaternion.identity);
                break;
        }
    }
}
