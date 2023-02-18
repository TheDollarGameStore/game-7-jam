using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject chaser;

    private void Start()
    {
        Invoke("Spawn", 2f);
    }

    void Spawn()
    {
        Invoke("Spawn", 2f);

        Vector3 spawnLocation = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 25f;

        Instantiate(chaser, spawnLocation + (Vector3.up * 1.5f), Quaternion.identity);
    }
}
