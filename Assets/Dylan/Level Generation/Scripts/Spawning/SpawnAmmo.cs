using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    public Transform[] spawnSpots;
    public GameObject ammoPrefab;
    private int randSpawnSpots;

    void Start()
    {

        randSpawnSpots = Random.Range(0, spawnSpots.Length);
        Instantiate(ammoPrefab, spawnSpots[randSpawnSpots].transform);

    }
}
