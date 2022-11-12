using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnSpot;
    public GameObject Player;
    private int randSpawnSpots;

    void Start()
    {

        randSpawnSpots = Random.Range(0, spawnSpot.Length);
        Instantiate(Player, spawnSpot[randSpawnSpots].transform);

    }
}
