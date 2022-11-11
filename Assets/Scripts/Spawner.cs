using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject Enemies;

    [SerializeField]bool time = true;
    [SerializeField]float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                GameObject bullets = Instantiate(Enemies, spawners[i].transform.position, Quaternion.identity);
                if(i >= 3)
                {
                    time = false;
                }
            }
            
        }
        if (!time)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                time = true;
            }
        }
    }
}
