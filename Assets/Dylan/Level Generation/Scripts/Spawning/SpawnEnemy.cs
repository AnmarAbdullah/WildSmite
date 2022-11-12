using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] spawnSpots;
    public GameObject enemyPrefab;
    private int randSpawnSpots;

    void Start()
    {

        randSpawnSpots = Random.Range(0, spawnSpots.Length);
        Instantiate(enemyPrefab, spawnSpots[randSpawnSpots].transform);

    }
}
